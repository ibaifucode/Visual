Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Xml
Imports Dropbox.Api
Imports Dropbox.Api.Files
Imports Octokit

Public Class Form1

    Private xmlDoc As New XmlDocument()
    Private carpetaTemporal As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Definir la estructura del DataGridView
        DataGridView1.Columns.Add("Ciudad", "Ciudad")
        DataGridView1.Columns.Add("Temperatura", "Temperatura ºC")
        DataGridView1.Columns.Add("Previsión", "Previsión")
        DataGridView1.Columns.Add("FuerzaViento", "Fuerza del Viento km/h")

    End Sub

    'TODOS LOS BOTONES QUE HAY Y SUS FUNCIONALIDADES

    Private Async Sub Button_Transformar_y_subir(sender As Object, e As EventArgs) Handles Button2.Click
        Dim carpetaXml As String = "../"
        Dim carpetaTxt As String = "../txt/"
        Dim token As String = "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"
        Dim owner As String = "ekagardu19"
        Dim repo As String = "RetoTemperatura"

        Await ConvertirXmlATxt(carpetaXml, carpetaTxt, token, owner, repo)

    End Sub

    Private Sub Button_Mirar_Prevision_de_una_Ciudad(sender As Object, e As EventArgs) Handles Button3.Click
        'Obtenemos la ciudad introducida por el usuario desde el TextBox
        Dim ciudad As String = tbmirarciudad.Text.Trim()

        ' Verificamos que la ciudad no esté vacía
        If String.IsNullOrEmpty(ciudad) Then
            MessageBox.Show("Por favor, ingrese una ciudad antes de consultar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Establecemos la dirección URL de la API y los parámetros de consulta con la ciudad introducida
        Dim apiUrl As String = "http://api.weatherapi.com/v1/current.xml"
        Dim key As String = "8d7ea9fe295245d7a33122706243001"
        Dim query As String = $"?key={key}&q={ciudad}&days=7&lang=es"

        Try
            Dim titulo As String = ciudad & ".xml"
            xmlDoc = New XmlDocument()
            ' Cargamos el documento XML desde la API usando el método Load
            xmlDoc.Load(apiUrl & query)

            ' Accedemos a los elementos y atributos del documento XML usando el método SelectSingleNode o SelectNodes
            Dim root As XmlNode = xmlDoc.DocumentElement ' Obtiene el elemento raíz

            ' Accede a los elementos específicos del XML que deseas mostrar en el DataGridView
            Dim temperatureNode As XmlNode = root.SelectSingleNode("//temp_c")
            Dim temperatura As String = temperatureNode.InnerText

            Dim conditionNode As XmlNode = root.SelectSingleNode("//condition/text")
            Dim previsión As String = conditionNode.InnerText

            Dim windNode As XmlNode = root.SelectSingleNode("//wind_kph")
            Dim fuerzaViento As String = windNode.InnerText

            ' Agrega una nueva fila al DataGridView con los datos obtenidos
            DataGridView1.Rows.Add(ciudad, temperatura, previsión, fuerzaViento)

            ' Guarda los datos en un archivo XML en la carpeta temporal
            ' GuardarDatosCiudadEnXML(ciudad, temperatura, previsión, fuerzaViento)
            tbmirarciudad.Text = ""
            xmlDoc.Save("../" & titulo)

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

    '---------------------------------------------------------- FUNCIONALIDADES --------------------------------------------------------------------


    Private Async Function ConvertirXmlATxt(ByVal carpetaXml As String, ByVal carpetaTxt As String, ByVal token As String, ByVal owner As String, ByVal repo As String) As Task
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

                ' Subir el archivo de texto a GitHub
                Await SubirArchivoAGitHub(archivoTxt, token, owner, repo)
            Catch ex As Exception
                ' Manejar errores
                Console.WriteLine($"Error al convertir '{archivoXml}': {ex.Message}")
            End Try
        Next

        MessageBox.Show("Proceso de conversión completado.")
    End Function

    Private Async Function SubirArchivoAGitHub(ByVal archivoTxt As String, ByVal token As String, ByVal owner As String, ByVal repo As String) As Task
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

            Dim fileContent As Byte() = File.ReadAllBytes(archivoTxt)
            Dim content As New MultipartFormDataContent()
            content.Add(New ByteArrayContent(fileContent), "file", Path.GetFileName(archivoTxt))

            Dim response = Await client.PutAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{Path.GetFileName(archivoTxt)}", content)

            If response.IsSuccessStatusCode Then
                MessageBox.Show($"Archivo '{archivoTxt}' subido exitosamente a GitHub.")
            Else
                MessageBox.Show($"Error al subir archivo '{archivoTxt}' a GitHub: {response.StatusCode} - {response.ReasonPhrase}")
            End If
        End Using
    End Function

End Class
