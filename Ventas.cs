using FontAwesome.Sharp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace ConexionSQL
{
    public partial class Ventas : Form
    {
        private Form FormularioVentasActual;
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        //private SqlConnection Conexion = new SqlConnection("Data Source=LATPTOP\\SQLSERVEREXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True"); //Salma
        private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True"); //Vanessita
        private Random id = new Random();
        private int currentIndex = 0;


        public Ventas()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);

            //form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            CargarDatos();
        }

        private void CargarDatos()
        {
            string query = "SELECT * FROM Ventas";
            SqlDataAdapter da = new SqlDataAdapter(query, Conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dtw_Ventas.DataSource = dt;
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
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //borde de boton
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
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void Reset()
        {
            DisableButtom();
            leftBorderBtn.Visible = false;
        }

        private void Limpiar()
        {
            txtZapatos.Clear();
            txtFecha.Clear();
            txtTotal.Clear();
            txtBuscarV.Clear();

            btnModificarV.Enabled = false;
            btnEliminarV.Enabled = false;
        }

        private void Busqueda(DataGridView d, int col)
        {
            string query = "";
            DateTime fechaVenta;

            // Formatos de fecha que aceptamos (puedes agregar más si es necesario)
            string[] formatosFecha = {
        "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "MM-dd-yyyy"
    };

            // Intentar parsear la fecha con diferentes formatos
            bool esFechaValida = DateTime.TryParseExact(txtBuscarV.Text.Trim(), formatosFecha,
                                                         System.Globalization.CultureInfo.InvariantCulture,
                                                         System.Globalization.DateTimeStyles.None, out fechaVenta);

            // Si el valor ingresado es una fecha válida
            if (!string.IsNullOrEmpty(txtBuscarV.Text.Trim()) && esFechaValida)
            {
                query = "SELECT * FROM Ventas WHERE Fecha = @Fecha";
            }
            // Si el valor ingresado es un número (por ejemplo, Total), buscamos por Total
            else if (!string.IsNullOrEmpty(txtBuscarV.Text.Trim()) && txtBuscarV.Text.Trim().All(char.IsDigit))
            {
                query = "SELECT * FROM Ventas WHERE Total = @Total";
            }
            else
            {
                MessageBox.Show("Por favor ingrese una fecha válida.", "\u26A0 Error de busqueda/Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlCommand cmd = new SqlCommand(query, Conexion);

            // Si es una fecha válida, agregamos el parámetro correspondiente
            if (esFechaValida)
            {
                cmd.Parameters.AddWithValue("@Fecha", fechaVenta.ToString("yyyy-MM-dd"));  // Convertir a formato SQL
            }
            else
            {
                // Si es un número, buscamos por Total
                cmd.Parameters.AddWithValue("@Total", txtBuscarV.Text.Trim());
            }

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dtw_Ventas.DataSource = dt;

            // Si se encuentran resultados, mostramos el primer registro
            if (dt.Rows.Count > 0)
            {
                CargarPrimerRegistro(0); // Mostrar el primer resultado encontrado
                btnModificarV.Enabled = true;
                btnEliminarV.Enabled = true;
            }
            else
            {
                MessageBox.Show("No se encontraron registros.", "\u26A0 Error de busqueda/Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
            }
        }

        private void CargarPrimerRegistro(int index)
        {
            // Verifica si hay filas en el DataGridView
            if (dtw_Ventas.Rows.Count > 0)
            {
                // Obtiene la fila correspondiente al índice
                DataGridViewRow row = dtw_Ventas.Rows[index];

                // Carga los datos de la fila en los TextBox
                lbl_ID.Text = row.Cells["ID_Venta"].Value.ToString();
                txtZapatos.Text = row.Cells["ID_Zapato"].Value.ToString();
                txtFecha.Text = row.Cells["Fecha"].Value.ToString();
                txtTotal.Text = row.Cells["Total"].Value.ToString();
            }
        }

        private void btnAgregarV_Click(object sender, EventArgs e)
        {
            string Zapatos = txtZapatos.Text;
            string Fecha = txtFecha.Text;
            string Total = txtTotal.Text;

            if (string.IsNullOrEmpty(Zapatos) || string.IsNullOrEmpty(Fecha) || string.IsNullOrEmpty(Total))
            {
                MessageBox.Show(this, "Campos incompletos", "\u26A0Error de validación-Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime fechaVenta;
            if (!DateTime.TryParse(Fecha, out fechaVenta))
            {
                MessageBox.Show("Fecha no válida. Por favor ingrese una fecha válida.", "\u26A0 Error Agregar/Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Generar un ID único para la venta
            int valor = id.Next(100, 999);
            lbl_ID.Text = "V" + valor.ToString();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Ventas (ID_Venta, ID_Zapato, Fecha, Total) VALUES (@ID_Venta, @ID_Zapato, @Fecha, @Total)",
                Conexion);

            cmd.Parameters.AddWithValue("@ID_Venta", lbl_ID.Text);
            cmd.Parameters.AddWithValue("@ID_Zapato", Zapatos);
            cmd.Parameters.AddWithValue("@Fecha", fechaVenta);
            cmd.Parameters.AddWithValue("@Total", Total);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Venta guardada correctamente.", "\u2705Venta Añadida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, "Error al registrar venta", "\u26AEError-Añadir venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos();
            Limpiar();
        }

        private void btnModificarV_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Ventas SET ID_Zapato = @ID_Zapato, Fecha = @Fecha, Total = @Total WHERE ID_Venta = @ID_Venta";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            DateTime fechaVenta;
            if (!DateTime.TryParse(txtFecha.Text, out fechaVenta))
            {
                MessageBox.Show("Fecha no válida. Por favor ingrese una fecha válida.", "\u26A0 Error Modificar/Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.Parameters.AddWithValue("@ID_Venta", lbl_ID.Text);
            cmd.Parameters.AddWithValue("@ID_Zapato", txtZapatos.Text);
            cmd.Parameters.AddWithValue("@Fecha", fechaVenta);
            cmd.Parameters.AddWithValue("@Total", txtTotal.Text);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cambio realizado en la base de datos", "\u2705Venta Modificada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar", "\u26A0 Error Modificar/Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos();
            Limpiar();
        }

        private void btnEliminarV_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Ventas WHERE ID_Venta = @ID_Venta";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            cmd.Parameters.AddWithValue("@ID_Venta", lbl_ID.Text);

            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Registro eliminado de la base de datos", "\u2705Venta Eliminada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar", "\u26A0 Error Eliminar/Venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
            }

            CargarDatos();
            Limpiar();
        }

        private void btnBuscarV_Click(object sender, EventArgs e)
        {
            if (txtBuscarV.Text != "")
            {
                Busqueda(dtw_Ventas, 0);
                btnAgregarV.Enabled = false;
                btnLimpiarV.Enabled = true;  // Activar el botón de limpiar
                btnLimpiarV.Visible = true;
            }
            else
            {
                MessageBox.Show("Error. Intentelo de nuevo :)", "\u26A0 Error de busqueda/Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiarV_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnAgregarV.Enabled = true;
            btnLimpiarV.Enabled = false;  // Desactivar el botón de limpiar
        }

        private void btnAtrasP_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                CargarPrimerRegistro(currentIndex);
            }
        }

        private void btnSiguienteP_Click(object sender, EventArgs e)
        {
            if (currentIndex < dtw_Ventas.Rows.Count - 1)
            {
                currentIndex++;
                CargarPrimerRegistro(currentIndex);
            }
        }
        private void btnUltimoV_Click(object sender, EventArgs e)
        {
            currentIndex = dtw_Ventas.Rows.Count - 1;
            CargarPrimerRegistro(currentIndex);

            btnSiguienteP.Enabled = false;
            btnAtrasP.Enabled = true;
        }

        private void btnPrimerV_Click(object sender, EventArgs e)
        {
            currentIndex = 0;
            CargarPrimerRegistro(currentIndex);

            btnAtrasP.Enabled = false;
            btnSiguienteP.Enabled = true;
        }


        private void Ventas_Load(object sender, EventArgs e)
        {
            //Cargar la lista de ventas en el DataGridView
            CargarDatos();
            btnLimpiarV.Visible = false;  // Asegúrate de que el botón sea visible
            btnLimpiarV.Enabled = false;  // Inicialmente desactivar el botón de limpiar
        
    }
    }
}






