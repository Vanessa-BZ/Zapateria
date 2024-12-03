using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class BotonRedondeado : Button
{
    public int Radio { get; set; } = 20; // Radio para esquinas redondeadas
    public Color ColorDeFondo { get; set; } = Color.LightPink; // Color de fondo
    public Color ColorBorde { get; set; } = Color.Black; // Color del borde
    public int GrosorBorde { get; set; } = 2; // Grosor del borde

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);

        // Crear la forma redondeada
        GraphicsPath path = new GraphicsPath();
        path.AddArc(new Rectangle(0, 0, Radio, Radio), 180, 90);
        path.AddArc(new Rectangle(Width - Radio, 0, Radio, Radio), 270, 90);
        path.AddArc(new Rectangle(Width - Radio, Height - Radio, Radio, Radio), 0, 90);
        path.AddArc(new Rectangle(0, Height - Radio, Radio, Radio), 90, 90);
        path.CloseFigure();

        // Asignar la región al botón
        this.Region = new Region(path);

        // Dibujar el fondo
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (SolidBrush brush = new SolidBrush(ColorDeFondo))
        {
            pevent.Graphics.FillPath(brush, path);
        }

        // Dibujar el borde
        using (Pen pen = new Pen(ColorBorde, GrosorBorde))
        {
            pevent.Graphics.DrawPath(pen, path);
        }

        // Dibujar el texto centrado
        TextRenderer.DrawText(
            pevent.Graphics,
            this.Text,
            this.Font,
            this.ClientRectangle,
            this.ForeColor,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
        );
    }
}