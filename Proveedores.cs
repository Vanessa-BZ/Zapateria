using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace ConexionSQL
{
    public partial class Proveedores : Form
    {
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");
        private Random id = new Random();

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

                // Borde del botón
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

            dtw_Proveedores.DataSource = dt;
        }

        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            string Nombre = txtNombreProv.Text;
            string Precio = txtPrecio.Text;
            string Unidades = txtUnidades.Text;
            string Comercio = CBXcomercio.Text;

            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Precio) || string.IsNullOrEmpty(Unidades) || string.IsNullOrEmpty(Comercio))
            {
                MessageBox.Show("Campos incompletos");
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
                MessageBox.Show("Proveedor agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el proveedor: {ex.Message}");
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
            string query = "UPDATE Proveedores SET Nombre_P = @Nombre_P, Precio = @Precio, Unidades = @Unidades, Comercializacion = @Comercializacion WHERE ID_Proveedor = @ID_Proveedor";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Proveedor", lbl_id.Text);
            cmd.Parameters.AddWithValue("@Nombre_P", txtNombreProv.Text);
            cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);
            cmd.Parameters.AddWithValue("@Unidades", txtUnidades.Text);
            cmd.Parameters.AddWithValue("@Comercializacion", CBXcomercio.Text);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro modificado en la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos(); // Recarga los datos después de modificar
            Limpiar();
        }

        private void btnEliminarP_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Proveedores WHERE ID_Proveedor = @ID_Proveedor";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Proveedor", lbl_id.Text);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado de la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos(); // Recarga los datos después de eliminar
            Limpiar();
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
                CargarPrimerRegistro();
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)");
            }

            // Limpiar el campo de búsqueda
            txtBuscarP.Clear();
        }

        private void CargarPrimerRegistro()
        {
            // Verifica si hay filas en el DataGridView
            if (dtw_Proveedores.Rows.Count > 0)
            {
                // Obtiene la primera fila del DataGridView
                DataGridViewRow row = dtw_Proveedores.Rows[0];

                // Carga los datos de la primera fila en los TextBox
                lbl_id.Text = row.Cells["ID_Proveedor"].Value.ToString();
                txtNombreProv.Text = row.Cells["Nombre_P"].Value.ToString();
                txtPrecio.Text = row.Cells["Precio"].Value.ToString();
                txtUnidades.Text = row.Cells["Unidades"].Value.ToString();
                CBXcomercio.Text = row.Cells["Comercializacion"].Value.ToString();
            }
        }
    }
}


