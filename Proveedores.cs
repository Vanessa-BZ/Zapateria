using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        string dato;
        int i;

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
            string query = "SELECT * FROM Proveedores WHERE ID_Proveedor = @ID_Proveedor";
            SqlCommand cmd = new SqlCommand(query, Conexion);
            cmd.Parameters.AddWithValue("@ID_Proveedor", txtBuscarP.Text.Trim());

            Conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbl_id.Text = reader["ID_Proveedor"].ToString();
                txtNombreProv.Text = reader["Nombre_P"].ToString();
                txtPrecio.Text = reader["Precio"].ToString();
                txtUnidades.Text = reader["Unidades"].ToString();
                CBXcomercio.Text = reader["Comercializacion"].ToString();
            }
            else
            {
                MessageBox.Show("Proveedor no encontrado.");
            }
            Conexion.Close();
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

            int valor = id.Next(100, 999);
            lbl_id.Text = "P" + valor.ToString();

            dtw_Proveedores.Rows.Add(lbl_id.Text, Nombre, Precio, Unidades, Comercio);
            MessageBox.Show("Datos agregados al listado temporal");
            Limpiar();
        }

        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            SqlCommand Agregar = new SqlCommand(
                "INSERT INTO Proveedores (ID_Proveedor, Nombre_P, Precio, Unidades, Comercializacion) VALUES (@ID_Proveedor, @Nombre_P, @Precio, @Unidades, @Comercializacion)",
                Conexion);

            Conexion.Open();

            try
            {
                foreach (DataGridViewRow row in dtw_Proveedores.Rows)
                {
                    if (row.IsNewRow) continue; // Ignora la última fila vacía

                    Agregar.Parameters.Clear();
                    Agregar.Parameters.AddWithValue("@ID_Proveedor", Convert.ToString(row.Cells["Column1"].Value));
                    Agregar.Parameters.AddWithValue("@Nombre_P", Convert.ToString(row.Cells["Column2"].Value));
                    Agregar.Parameters.AddWithValue("@Precio", Convert.ToDecimal(row.Cells["Column3"].Value));
                    Agregar.Parameters.AddWithValue("@Unidades", Convert.ToInt32(row.Cells["Column4"].Value));
                    Agregar.Parameters.AddWithValue("@Comercializacion", Convert.ToString(row.Cells["Column5"].Value));

                    Agregar.ExecuteNonQuery();
                }
                MessageBox.Show("Datos guardados en la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en la base de datos: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }
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
                Busqueda(dtw_Proveedores, 0);
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)");
            }
            txtBuscarP.Clear();

            btnModificarP.Enabled = true;
            btnEliminarP.Enabled = true;
        }
    }
}