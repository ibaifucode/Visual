<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btEnviarSeleccionados = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.combobox = New System.Windows.Forms.ComboBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblTempActual = New System.Windows.Forms.Label()
        Me.paneltexto = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblCiudadActual = New System.Windows.Forms.Label()
        Me.panelPrevision = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblPrevision = New System.Windows.Forms.Label()
        Me.btAnadirEnInforme = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.paneltexto.SuspendLayout()
        Me.panelPrevision.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.Padding = New System.Windows.Forms.Padding(2)
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder
        Me.DataGridView1.Location = New System.Drawing.Point(-6, 430)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(807, 269)
        Me.DataGridView1.TabIndex = 1
        '
        'btEnviarSeleccionados
        '
        Me.btEnviarSeleccionados.BackColor = System.Drawing.SystemColors.HighlightText
        Me.btEnviarSeleccionados.Location = New System.Drawing.Point(620, 9)
        Me.btEnviarSeleccionados.Name = "btEnviarSeleccionados"
        Me.btEnviarSeleccionados.Size = New System.Drawing.Size(164, 22)
        Me.btEnviarSeleccionados.TabIndex = 2
        Me.btEnviarSeleccionados.Text = "PUBLICAR INFORME"
        Me.btEnviarSeleccionados.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Button3.Location = New System.Drawing.Point(221, 11)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(51, 22)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Buscar"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(11, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Ciudad"
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Button4.Location = New System.Drawing.Point(481, 9)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(133, 22)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "ELIMINAR"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.combobox)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Location = New System.Drawing.Point(1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 43)
        Me.Panel1.TabIndex = 8
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Button1.Location = New System.Drawing.Point(746, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(51, 22)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "Salir"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'combobox
        '
        Me.combobox.FormattingEnabled = True
        Me.combobox.Location = New System.Drawing.Point(82, 12)
        Me.combobox.Name = "combobox"
        Me.combobox.Size = New System.Drawing.Size(133, 21)
        Me.combobox.TabIndex = 11
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(0, 39)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(800, 352)
        Me.Panel3.TabIndex = 10
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.btEnviarSeleccionados)
        Me.Panel2.Location = New System.Drawing.Point(1, 387)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(800, 43)
        Me.Panel2.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Symbol", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Menu
        Me.Label2.Location = New System.Drawing.Point(11, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 21)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "MI INFORME"
        '
        'lblTempActual
        '
        Me.lblTempActual.AutoSize = True
        Me.lblTempActual.BackColor = System.Drawing.SystemColors.Control
        Me.lblTempActual.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTempActual.ForeColor = System.Drawing.SystemColors.Control
        Me.lblTempActual.Location = New System.Drawing.Point(3, 0)
        Me.lblTempActual.Name = "lblTempActual"
        Me.lblTempActual.Size = New System.Drawing.Size(167, 73)
        Me.lblTempActual.TabIndex = 11
        Me.lblTempActual.Text = "31 C"
        '
        'paneltexto
        '
        Me.paneltexto.BackColor = System.Drawing.Color.White
        Me.paneltexto.Controls.Add(Me.lblTempActual)
        Me.paneltexto.Controls.Add(Me.lblCiudadActual)
        Me.paneltexto.Location = New System.Drawing.Point(1, 41)
        Me.paneltexto.Margin = New System.Windows.Forms.Padding(0)
        Me.paneltexto.Name = "paneltexto"
        Me.paneltexto.Size = New System.Drawing.Size(604, 74)
        Me.paneltexto.TabIndex = 13
        Me.paneltexto.WrapContents = False
        '
        'lblCiudadActual
        '
        Me.lblCiudadActual.AutoSize = True
        Me.lblCiudadActual.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lblCiudadActual.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCiudadActual.ForeColor = System.Drawing.SystemColors.Control
        Me.lblCiudadActual.Location = New System.Drawing.Point(176, 0)
        Me.lblCiudadActual.Name = "lblCiudadActual"
        Me.lblCiudadActual.Size = New System.Drawing.Size(380, 37)
        Me.lblCiudadActual.TabIndex = 12
        Me.lblCiudadActual.Text = "NINGUNA CIUDAD BUSCADA"
        '
        'panelPrevision
        '
        Me.panelPrevision.Controls.Add(Me.lblPrevision)
        Me.panelPrevision.Location = New System.Drawing.Point(1, 115)
        Me.panelPrevision.Name = "panelPrevision"
        Me.panelPrevision.Size = New System.Drawing.Size(797, 55)
        Me.panelPrevision.TabIndex = 15
        '
        'lblPrevision
        '
        Me.lblPrevision.AutoSize = True
        Me.lblPrevision.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lblPrevision.Font = New System.Drawing.Font("Segoe UI Semibold", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrevision.ForeColor = System.Drawing.SystemColors.Control
        Me.lblPrevision.Location = New System.Drawing.Point(3, 0)
        Me.lblPrevision.Name = "lblPrevision"
        Me.lblPrevision.Padding = New System.Windows.Forms.Padding(3)
        Me.lblPrevision.Size = New System.Drawing.Size(386, 43)
        Me.lblPrevision.TabIndex = 15
        Me.lblPrevision.Text = "NINGUNA CIUDAD BUSCADA"
        '
        'btAnadirEnInforme
        '
        Me.btAnadirEnInforme.BackColor = System.Drawing.SystemColors.HighlightText
        Me.btAnadirEnInforme.Location = New System.Drawing.Point(634, 48)
        Me.btAnadirEnInforme.Name = "btAnadirEnInforme"
        Me.btAnadirEnInforme.Size = New System.Drawing.Size(164, 22)
        Me.btAnadirEnInforme.TabIndex = 16
        Me.btAnadirEnInforme.Text = "ANADIR AL INFORME"
        Me.btAnadirEnInforme.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(800, 700)
        Me.Controls.Add(Me.btAnadirEnInforme)
        Me.Controls.Add(Me.panelPrevision)
        Me.Controls.Add(Me.paneltexto)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DataGridView1)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.paneltexto.ResumeLayout(False)
        Me.paneltexto.PerformLayout()
        Me.panelPrevision.ResumeLayout(False)
        Me.panelPrevision.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btEnviarSeleccionados As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblTempActual As Label
    Friend WithEvents paneltexto As FlowLayoutPanel
    Friend WithEvents lblCiudadActual As Label
    Friend WithEvents panelPrevision As FlowLayoutPanel
    Friend WithEvents lblPrevision As Label
    Friend WithEvents btAnadirEnInforme As Button
    Friend WithEvents combobox As ComboBox
    Friend WithEvents Button1 As Button
End Class
