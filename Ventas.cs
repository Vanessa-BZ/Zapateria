﻿using FontAwesome.Sharp;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ConexionSQL
{
    public partial class Ventas : Form
    {
        private Form FormularioVentasActual;
        private Panel leftBorderBtn;
        private IconButton currentBtn;
        private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-L2KNQNU\\SQLEXPRESS; Initial Catalog=Inventario_Zapateria; Integrated Security=True");
        private Random id = new Random();

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

            btnModificarV.Enabled = false;
            btnEliminarV.Enabled = false;
        }

        private void Busqueda(DataGridView d, int col)
        {
            string query = "SELECT * FROM Ventas WHERE ID_Venta = @ID_Venta";
            SqlCommand cmd = new SqlCommand(query, Conexion);
            cmd.Parameters.AddWithValue("@ID_Venta", txtBuscarV.Text.Trim());

            Conexion.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbl_ID.Text = reader["ID_Venta"].ToString();
                txtZapatos.Text = reader["ID_Zapato"].ToString();
                txtFecha.Text = reader["Fecha"].ToString();
                txtTotal.Text = reader["Total"].ToString();
            }
            else
            {
                MessageBox.Show("Registro de venta no encontrado.");
            }
            Conexion.Close();
        }

        private void btnAgregarV_Click(object sender, EventArgs e)
        {
            string Zapatos = txtZapatos.Text;
            string Fecha = txtFecha.Text;
            string Total = txtTotal.Text;

            if (string.IsNullOrEmpty(Zapatos) || string.IsNullOrEmpty(Fecha) || string.IsNullOrEmpty(Total))
            {
                MessageBox.Show("Campos incompletos");
                return;
            }

            // Intentar convertir la fecha a un formato válido
            DateTime fechaVenta;
            if (!DateTime.TryParse(Fecha, out fechaVenta))
            {
                MessageBox.Show("Fecha no válida. Por favor ingrese una fecha válida.");
                return;
            }

            // Generar un ID único para la venta
            int valor = id.Next(100, 999);
            lbl_ID.Text = "V" + valor.ToString();

            // Preparar la consulta para insertar la venta
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Ventas (ID_Venta, ID_Zapato, Fecha, Total) VALUES (@ID_Venta, @ID_Zapato, @Fecha, @Total)",
                Conexion);

            // Asignar parámetros
            cmd.Parameters.AddWithValue("@ID_Venta", lbl_ID.Text);
            cmd.Parameters.AddWithValue("@ID_Zapato", Zapatos);
            cmd.Parameters.AddWithValue("@Fecha", fechaVenta);
            cmd.Parameters.AddWithValue("@Total", Total);

            // Ejecutar consulta
            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Venta guardada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la venta: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            // Recargar datos
            CargarDatos();
            Limpiar();
        }

        private void btnModificarV_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Ventas SET ID_Zapato = @ID_Zapato, Fecha = @Fecha, Total = @Total WHERE ID_Venta = @ID_Venta";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            // Intentar convertir la fecha a un formato válido
            DateTime fechaVenta;
            if (!DateTime.TryParse(txtFecha.Text, out fechaVenta))
            {
                MessageBox.Show("Fecha no válida. Por favor ingrese una fecha válida.");
                return;
            }

            // Asignar parámetros
            cmd.Parameters.AddWithValue("@ID_Venta", lbl_ID.Text);
            cmd.Parameters.AddWithValue("@ID_Zapato", txtZapatos.Text);
            cmd.Parameters.AddWithValue("@Fecha", fechaVenta);
            cmd.Parameters.AddWithValue("@Total", txtTotal.Text);

            // Ejecutar consulta
            Conexion.Open();
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cambio realizado en la base de datos");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}");
            }
            finally
            {
                Conexion.Close();
            }

            // Recargar datos
            CargarDatos();
            Limpiar();
        }

        private void btnEliminarV_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM Ventas WHERE ID_Venta = @ID_Venta";
            SqlCommand cmd = new SqlCommand(query, Conexion);

            // Asignar parámetro
            cmd.Parameters.AddWithValue("@ID_Venta", lbl_ID.Text);

            // Ejecutar consulta
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

            // Recargar datos
            CargarDatos();
            Limpiar();
        }

        private void btnBuscarV_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscarV.Text))
            {
                // Limpiar la conexión antes de realizar la búsqueda
                Conexion.Close();
                Conexion.Open();

                // Consulta para buscar por Fecha o Total, sin incluir ID_Venta o ID_Zapato
                string query = "SELECT * FROM Ventas WHERE Fecha LIKE @Buscar OR Total LIKE @Buscar";
                SqlCommand cmd = new SqlCommand(query, Conexion);
                cmd.Parameters.AddWithValue("@Buscar", "%" + txtBuscarV.Text.Trim() + "%");

                // Crear el DataTable para almacenar los resultados filtrados
                DataTable dt = new DataTable();

                // Usar SqlDataAdapter para llenar el DataTable con los resultados de la búsqueda
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                // Verificar si hay resultados
                if (dt.Rows.Count > 0)
                {
                    // Asignar los valores del primer registro a los TextBox
                    DataRow row = dt.Rows[0]; // Obtener el primer registro
                    lbl_ID.Text = row["ID_Venta"].ToString();
                    txtZapatos.Text = row["ID_Zapato"].ToString();
                    txtFecha.Text = row["Fecha"].ToString();
                    txtTotal.Text = row["Total"].ToString();

                    // Deshabilitar el botón de agregar después de realizar la búsqueda
                    btnAgregarV.Enabled = false;
                    btnModificarV.Enabled = true;
                    btnEliminarV.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se encontró ninguna venta con los datos proporcionados.");
                }

                Conexion.Close();
            }
            else
            {
                MessageBox.Show("Debe ingresar un valor para buscar.");
            }
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
        }
    }
}








