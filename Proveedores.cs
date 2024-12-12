using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;

namespace ConexionSQL
{
    public partial class Proveedores : Form
    {
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        private SqlConnection Conexion = new SqlConnection("Data Source=LATPTOP\\SQLSERVEREXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");  //Salma
        //private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");  //Vanessita
        private Random id = new Random();
        private int currentIndex = 0;


        public Proveedores()
        {
            InitializeComponent();

            // Configuración inicial del formulario
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);

            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            // Cargar datos de la base de datos al iniciar el formulario
            CargarDatos();

        }

        // Método para cargar los datos de la base de datos en el DataGridView
        private void CargarDatos()
        {
            string query = "SELECT * FROM Proveedores";
            SqlDataAdapter da = new SqlDataAdapter(query, Conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dtw_Proveedores.DataSource = dt;
        }

        private void ActivateButtom(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButtom();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(240, 128, 128);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;

                // Bor de del botón
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButtom()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(255, 128, 128);
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void Limpiar()
        {
            txtNombreProv.Clear();
            txtPrecio.Clear();
            txtUnidades.Clear();
            CBXcomercio.SelectedIndex = -1;

            btnModificarP.Enabled = false;
            btnEliminarP.Enabled = false;

            btnLimpiarP.Visible = false;
            btnAtrasP.Enabled = false;
            btnSiguienteP.Enabled = false;
            btnPrimerP.Enabled = false;
            btnUltimoP.Enabled = false;
        }

        // Método modificado para hacer la búsqueda en la base de datos
        private void Busqueda(DataGridView d, int col)
        {
            string query = "SELECT * FROM Proveedores WHERE ID_Proveedor LIKE @ID_Proveedor OR Nombre_P LIKE @Nombre_P OR Precio LIKE @Precio OR Unidades LIKE @Unidades OR Comercializacion LIKE @Comercializacion";
            SqlCommand cmd = new SqlCommand(query, Conexion);
            cmd.Parameters.AddWithValue("@ID_Proveedor", "%" + txtBuscarP.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Nombre_P", "%" + txtBuscarP.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Precio", "%" + txtBuscarP.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Unidades", "%" + txtBuscarP.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@Comercializacion", "%" + txtBuscarP.Text.Trim() + "%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            // Verifica si se encontraron registros
            if (dt.Rows.Count > 0)
            {
                d.DataSource = dt;
                currentIndex = 0; // Reinicia el índice
                CargarPrimerRegistro(currentIndex); // Carga el primer registro encontrado
            }
            else
            {
                MessageBox.Show("No se encontraron registros.", "\u26A0 Error de busqueda/Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar(); // Limpia los campos si no hay registros
            }
        }

        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            string Nombre = txtNombreProv.Text.ToUpper();
            string Precio = txtPrecio.Text;
            string Unidades = txtUnidades.Text;
            string Comercio = CBXcomercio.Text;

            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Precio) || string.IsNullOrEmpty(Unidades) || string.IsNullOrEmpty(Comercio))
            {
               MessageBox.Show(this, "Campos incompletos", "\u26A0 Error de validación/Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
               
            }

            // Generar un ID único para el nuevo proveedor
            int valor = id.Next(100, 999);
            lbl_id.Text = "P" + valor.ToString();

            // Preparar la consulta para insertar el nuevo proveedor en la base de datos
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Proveedores (ID_Proveedor, Nombre_P, Precio, Unidades, Comercializacion) VALUES (@ID_Proveedor, @Nombre_P, @Precio, @Unidades, @Comercializacion)",
                Conexion);

            // Asignar parámetros a la consulta
            cmd.Parameters.AddWithValue("@ID_Proveedor", lbl_id.Text);
            cmd.Parameters.AddWithValue("@Nombre_P", Nombre);
            cmd.Parameters.AddWithValue("@Precio", Precio);
            cmd.Parameters.AddWithValue("@Unidades", Unidades);
            cmd.Parameters.AddWithValue("@Comercializacion", Comercio);

            // Ejecutar la consulta para insertar los datos
            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(this, "Proveedor agregado correctamente", "\u2705Proveedor Añadido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error: No se puede agregar el proveedor", "\u26AEError-Añadir Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
            }

            // Recargar los datos en el DataGridView para mostrar el nuevo proveedor
            CargarDatos(); // Este método recarga todos los proveedores
            Limpiar(); // Limpiar los campos de texto
        }

        private void btnModificarP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbl_id.Text))
            {
                MessageBox.Show(this,"Por favor seleccione un proveedor para modificar.", "\u26AEError- Modificar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "UPDATE Proveedores SET Nombre_P = @Nombre_P, Precio = @Precio, Unidades = @Unidades, Comercializacion = @Comercializacion WHERE ID_Proveedor = @ID_Proveedor";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Proveedor", lbl_id.Text);
            cmd.Parameters.AddWithValue("@Nombre_P", txtNombreProv.Text.ToUpper());
            cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
            cmd.Parameters.AddWithValue("@Unidades", txtUnidades.Text);
            cmd.Parameters.AddWithValue("@Comercializacion", CBXcomercio.Text);

            Conexion.Open();
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registro modificado en la base de datos","\u2705Proveedor Modificado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el registro para modificar.", "\u26AEError-Modificar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos();
            Limpiar();
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbl_id.Text))
            {
                MessageBox.Show("Por favor seleccione un proveedor para eliminar.", "\u26AEError- Eliminar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string query = "DELETE FROM Proveedores WHERE ID_Proveedor = @ID_Proveedor";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Proveedor", lbl_id.Text);

            Conexion.Open();
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registro eliminado de la base de datos", "\u2705Proveedor Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos(); // Recargar datos después de eliminar
                    Limpiar(); // Limpiar campos
                }
                else
                {
                    MessageBox.Show("No se encontró el registro para eliminar.", "\u26AEError-Eliminar Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }
        }

        private void btnBuscarP_Click(object sender, EventArgs e)
        {
            if (txtBuscarP.Text != "")
            {
                // Realiza la búsqueda
                Busqueda(dtw_Proveedores, 0);

                // Desactiva el botón de agregar para evitar duplicados
                btnAgregarP.Enabled = false;

                // Activa los botones de modificar y eliminar
                btnModificarP.Enabled = true;
                btnEliminarP.Enabled = true;

                // Asegura que el primer registro se cargue en los TextBox
                CargarPrimerRegistro(currentIndex);

                // Limpiar el campo de búsqueda
                txtBuscarP.Clear();
                btnLimpiarP.Visible = true;
                btnAtrasP.Enabled = true;
                btnSiguienteP.Enabled = true;
                btnPrimerP.Enabled = true;
                btnUltimoP.Enabled = true;
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)", "\u26A0 Error de busqueda/Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void CargarPrimerRegistro(int index)
        {
            // Verifica si hay filas en el DataGridView
            if (dtw_Proveedores.Rows.Count > 0)
            {
                // Validar el índice antes de acceder a la fila
                if (index >= 0 && index < dtw_Proveedores.Rows.Count)
                {
                    // Obtiene la fila del DataGridView
                    DataGridViewRow row = dtw_Proveedores.Rows[index];

                    // Carga los datos de la fila en los TextBox
                    lbl_id.Text = row.Cells["ID_Proveedor"].Value.ToString();
                    txtNombreProv.Text = row.Cells["Nombre_P"].Value.ToString();
                    txtPrecio.Text = row.Cells["Precio"].Value.ToString();
                    txtUnidades.Text = row.Cells["Unidades"].Value.ToString();
                    CBXcomercio.Text = row.Cells["Comercializacion"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Índice inválido.");
                }
            }
            else
            {
                MessageBox.Show("El DataGridView está vacío.");
            }
        }

        private void btnLimpiarP_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnAgregarP.Enabled = true;
        }

        private void btnAtrasP_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                CargarPrimerRegistro(currentIndex);

                // Enable/Disable navigation buttons
                btnAtrasP.Enabled = true;
            }
            else
            {
                btnAtrasP.Enabled = false;
            }
        }

        private void btnSiguienteP_Click(object sender, EventArgs e)
        {
            if (currentIndex < dtw_Proveedores.Rows.Count - 1)
            {
                currentIndex++;
                CargarPrimerRegistro(currentIndex);

                btnAtrasP.Enabled = true;
            }
            else
            {
                btnSiguienteP.Enabled = false;
            }
        }

        private void btnUltimoP_Click(object sender, EventArgs e)
        {
            currentIndex = dtw_Proveedores.Rows.Count - 1;
            CargarPrimerRegistro(currentIndex);

            btnSiguienteP.Enabled = false;
            btnAtrasP.Enabled = true;
        }

        private void btnPrimerP_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            CargarPrimerRegistro(currentIndex);

            btnAtrasP.Enabled = false;
            btnSiguienteP.Enabled = true;
        }
    }
}


