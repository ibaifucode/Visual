<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ApiUrlTextBox = New System.Windows.Forms.TextBox()
        Me.LocalFilePathTextBox = New System.Windows.Forms.TextBox()
        Me.DropboxPathTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(332, 87)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(153, 61)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "OBTENER METEOROLOGIA"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ApiUrlTextBox
        '
        Me.ApiUrlTextBox.Location = New System.Drawing.Point(97, 215)
        Me.ApiUrlTextBox.Name = "ApiUrlTextBox"
        Me.ApiUrlTextBox.Size = New System.Drawing.Size(151, 20)
        Me.ApiUrlTextBox.TabIndex = 1
        '
        'LocalFilePathTextBox
        '
        Me.LocalFilePathTextBox.Location = New System.Drawing.Point(332, 215)
        Me.LocalFilePathTextBox.Name = "LocalFilePathTextBox"
        Me.LocalFilePathTextBox.Size = New System.Drawing.Size(151, 20)
        Me.LocalFilePathTextBox.TabIndex = 2
        '
        'DropboxPathTextBox
        '
        Me.DropboxPathTextBox.Location = New System.Drawing.Point(553, 215)
        Me.DropboxPathTextBox.Name = "DropboxPathTextBox"
        Me.DropboxPathTextBox.Size = New System.Drawing.Size(151, 20)
        Me.DropboxPathTextBox.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(104, 199)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "URL de la API meteorológica"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(329, 199)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(156, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Ruta local donde se guardará   "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(531, 199)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(199, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Ruta en Dropbox (servicio seleccionado)"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DropboxPathTextBox)
        Me.Controls.Add(Me.LocalFilePathTextBox)
        Me.Controls.Add(Me.ApiUrlTextBox)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents ApiUrlTextBox As TextBox
    Friend WithEvents LocalFilePathTextBox As TextBox
    Friend WithEvents DropboxPathTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
