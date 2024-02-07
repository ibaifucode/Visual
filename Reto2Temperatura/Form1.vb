Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Xml
Imports AngleSharp.Dom
Imports CG.Web.MegaApiClient
Imports MegaApiClient
Imports Google.Apis.Drive.v3
Imports INode = AngleSharp.Dom.INode

Public Class Form1

    Private xmlDoc As New XmlDocument()
    Private carpetaTemporal As String

    Dim temperaturaActual As String
    Dim previsionActual As String
    Dim fuerzaVientoActual As String
    Dim ciudadActual As String


    Dim carpetaXml As String = "../"
    Dim carpetaTxt As String = "../txt/"
    Dim megaUsername As String = "dam3.ekaitz.garduno@gmail.com"
    Dim megaPassword As String = "@Sestao04"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Definir la estructura del DataGridView
        DataGridView1.Columns.Add("Ciudad", "Ciudad")
        DataGridView1.Columns.Add("Temperatura", "Temperatura ºC")
        DataGridView1.Columns.Add("Previsión", "Previsión")
        DataGridView1.Columns.Add("FuerzaViento", "Fuerza del Viento km/h")

        lblTempActual.Visible = False
        paneltexto.BackColor = Nothing
        lblCiudadActual.BackColor = Nothing
        lblTempActual.BackColor = Nothing
        lblPrevision.BackColor = Nothing
        btAnadirEnInforme.Visible = False
        panelPrevision.BackColor = Nothing

        combobox.Items.Add("Bilbao")
        combobox.Items.Add("Sestao")
        combobox.Items.Add("Santander")
        combobox.Items.Add("Vitoria-Gasteiz")

        combobox.DropDownStyle = ComboBoxStyle.DropDownList

        IniciarSesionMega()


    End Sub

    'TODOS LOS BOTONES QUE HAY Y SUS FUNCIONALIDADES

    Private Async Sub Button_Transformar_y_subir(sender As Object, e As EventArgs) Handles btEnviarSeleccionados.Click

        IniciarSesionMega()

        lblCiudadActual.BackColor = Color.FromArgb(0, 0, 0, 0) ' Transparente
        lblTempActual.BackColor = Color.FromArgb(0, 0, 0, 0)  ' Transparente
        lblCiudadActual.ForeColor = Color.White
        lblTempActual.ForeColor = Color.White
        paneltexto.BackColor = Color.FromArgb(0, 0, 0, 0)
        panelPrevision.BackColor = Color.FromArgb(0, 0, 0, 0)

        Await ConvertirXmlATxt(carpetaXml, carpetaTxt)



    End Sub

    Private Sub Button_Mirar_Prevision_de_una_Ciudad(sender As Object, e As EventArgs) Handles Button3.Click
        'Obtenemos la ciudad introducida por el usuario desde el TextBox
        ciudadActual = combobox.Text.Trim


        ' Verificamos que la ciudad no esté vacía
        If String.IsNullOrEmpty(ciudadActual) Then
            MessageBox.Show("Por favor, ingrese una ciudad antes de consultar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim apiUrl As String = "http://api.weatherapi.com/v1/current.xml"
        Dim key As String = "8d7ea9fe295245d7a33122706243001"
        Dim query As String = $"?key={key}&q={ciudadActual}&days=7&lang=es"

        Try
            lblTempActual.Visible = True

            xmlDoc = New XmlDocument()
            xmlDoc.Load(apiUrl & query)

            Dim root As XmlNode = xmlDoc.DocumentElement


            Dim temperatureNode As XmlNode = root.SelectSingleNode("//temp_c")
            temperaturaActual = temperatureNode.InnerText

            Dim conditionNode As XmlNode = root.SelectSingleNode("//condition/text")
            previsionActual = conditionNode.InnerText

            Dim windNode As XmlNode = root.SelectSingleNode("//wind_kph")
            fuerzaVientoActual = windNode.InnerText

            lblCiudadActual.Text = ciudadActual.ToUpper
            lblTempActual.Text = temperaturaActual + " ºC"
            lblPrevision.Text = previsionActual

            If previsionActual.IndexOf("Soleado", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Me.BackgroundImage = My.Resources.soleado
            ElseIf previsionActual.IndexOf("nublado", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Me.BackgroundImage = My.Resources.nub
            ElseIf previsionActual.IndexOf("despejado", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Me.BackgroundImage = My.Resources.despejado
            ElseIf previsionActual.IndexOf("lluvia", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Me.BackgroundImage = My.Resources.lluvia
            ElseIf previsionActual.IndexOf("neblina", StringComparison.OrdinalIgnoreCase) >= 0 Or previsionActual.IndexOf("niebla", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Me.BackgroundImage = My.Resources.niebla
            ElseIf previsionActual.IndexOf("lluvia", StringComparison.OrdinalIgnoreCase) >= 0 Then
                Me.BackgroundImage = My.Resources.lluvia
            End If

            lblCiudadActual.BackColor = Color.FromArgb(0, 0, 0, 0)
            lblTempActual.BackColor = Color.FromArgb(0, 0, 0, 0)
            paneltexto.BackColor = Color.FromArgb(0, 0, 0, 0)
            panelPrevision.BackColor = Color.FromArgb(0, 0, 0, 0)
            btAnadirEnInforme.Visible = True

        Catch ex As Exception
            MessageBox.Show($"Error al obtener la información del tiempo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Button_Eliminar(sender As Object, e As EventArgs) Handles Button4.Click
        ' Verificamos si hay al menos una fila seleccionada
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Mostramos un mensaje de confirmación antes de eliminar las filas
            Dim result As DialogResult = MessageBox.Show("¿Está seguro de que desea eliminar las filas seleccionadas?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Eliminamos todas las filas seleccionadas
                For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
                    Dim titulo As String = selectedRow.Cells("Ciudad").Value.ToString()

                    ' Eliminar el archivo XML correspondiente
                    Dim rutaArchivoXML As String = Path.Combine("../", titulo & ".xml")
                    If File.Exists(rutaArchivoXML) Then
                        File.Delete(rutaArchivoXML)
                    End If

                    ' Eliminar el archivo XML correspondiente
                    Dim rutaArchivotxt As String = Path.Combine("../txt/", titulo & ".txt")
                    If File.Exists(rutaArchivotxt) Then
                        File.Delete(rutaArchivotxt)
                    End If


                    ' Eliminar la fila del DataGridView
                    DataGridView1.Rows.Remove(selectedRow)
                Next
            End If
        Else
            MessageBox.Show("Por favor, seleccione al menos una fila antes de intentar eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button_Salir(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub
    Private Sub btAnadirEnInforme_Click(sender As Object, e As EventArgs) Handles btAnadirEnInforme.Click
        anadirTiempoEnInforme()
    End Sub

    '---------------------------------------------------------- FUNCIONALIDADES --------------------------------------------------------------------

    Private Async Function ConvertirXmlATxt(ByVal carpetaXml As String, ByVal carpetaTxt As String) As Task
        ' Obtener la lista de archivos XML en la carpeta
        Dim archivosXml As String() = Directory.GetFiles(carpetaXml, "*.xml")

        ' Recorrer cada archivo XML
        For Each archivoXml As String In archivosXml
            Try
                ' Cargar el archivo XML
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(archivoXml)

                ' Crear un nombre de archivo de texto basado en el archivo XML
                Dim archivoTxt As String = Path.Combine(carpetaTxt, Path.GetFileNameWithoutExtension(archivoXml) & ".txt")

                ' Crear un escritor de texto para el archivo de texto
                Using writer As New StreamWriter(archivoTxt)
                    ' Escribir el contenido XML en el archivo de texto
                    writer.Write(xmlDoc.OuterXml)
                End Using

                ' Mensaje de éxito
                Console.WriteLine($"Se convirtió '{archivoXml}' a '{archivoTxt}'")

            Catch ex As Exception
                ' Manejar errores
                Console.WriteLine($"Error al convertir '{archivoXml}': {ex.Message}")
            End Try
        Next


        MessageBox.Show("Proceso de conversión completado.")
    End Function

    Private Sub anadirTiempoEnInforme()

        DataGridView1.Rows.Add(ciudadActual, temperaturaActual, previsionActual, fuerzaVientoActual)
        xmlDoc.Save("../" & ciudadActual & ".xml")

    End Sub

    Private Sub IniciarSesionMega()
        Dim carpetaTxt As String = "../txt/"
        Dim megaApiClient As New MegaApiClient()
        Try
            megaApiClient.Login(megaUsername, megaPassword)
            SubirArchivoAMega(megaApiClient, carpetaTxt)
            megaApiClient.Logout()
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub
    Sub SubirArchivoAMega(ByVal megaApiClient As MegaApiClient, ByVal filePath As String)
        Using fileStream As New FileStream(filePath, FileMode.Open)
            Dim fileName As String = Path.GetFileName(filePath)
            Dim parentNode As INode = megaApiClient.GetNodes().First()
            megaApiClient.Upload(fileStream, fileName, parentNode)
        End Using
        MessageBox.Show("Archivo subido exitosamente a Mega.")
    End Sub

End Class
