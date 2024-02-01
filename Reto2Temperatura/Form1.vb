Imports System.IO
Imports System.Net
Imports System.Xml
Imports Dropbox.Api
Imports Dropbox.Api.Files

Public Class Form1

    Private xmlDoc As New XmlDocument()
    Private carpetaTemporal As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '' Establece la dirección URL de la API y los parámetros de consulta
        'Dim apiUrl As String = "http://api.weatherapi.com/v1/current.xml"
        'Dim key As String = ""
        'Dim query As String = "?key=8d7ea9fe295245d7a33122706243001&q=Sestao&days=7&lang=es"

        '' Carga el documento XML desde la API usando el método Load
        'xmlDoc.Load(apiUrl & query)

        '' Accede a los elementos y atributos del documento XML usando el método SelectSingleNode o SelectNodes
        'Dim root As System.Xml.XmlNode = xmlDoc.DocumentElement ' Obtiene el elemento raíz
        'Dim elements As System.Xml.XmlNodeList = root.ChildNodes ' Obtiene los elementos hijos del elemento raíz
        'Dim attribute As System.Xml.XmlAttribute = root.Attributes("id") ' Obtiene el atributo id del elemento raíz

        '' Haz algo con el documento XML, por ejemplo, mostrarlo en la consola
        'xmlDoc.Save("../Reto2Temperatura.xml")

        ' Crear una carpeta temporal al iniciar la aplicación
        'carpetaTemporal = Path.Combine(Path.GetTempPath(), "DatosCiudadesTemp")
        'Directory.CreateDirectory(carpetaTemporal)

        ' Definir la estructura del DataGridView
        DataGridView1.Columns.Add("Ciudad", "Ciudad")
        DataGridView1.Columns.Add("Temperatura", "Temperatura ºC")
        DataGridView1.Columns.Add("Previsión", "Previsión")
        DataGridView1.Columns.Add("FuerzaViento", "Fuerza del Viento km/h")





    End Sub

    'TODOS LOS BOTONES QUE HAY Y SUS FUNCIONALIDADES

    Private Sub Button_Transformar_y_subir(sender As Object, e As EventArgs) Handles Button2.Click

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

End Class

'Private Sub SubirArchivosAMega()
'    ' Verifica si hay al menos una fila seleccionada
'    If DataGridView1.SelectedRows.Count > 0 Then
'        ' Itera sobre las filas seleccionadas
'        For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
'            ' Genera el nombre del archivo XML utilizando el nombre de la ciudad
'            Dim ciudad As String = selectedRow.Cells("Ciudad").Value.ToString()
'            Dim nombreArchivoTxt As String = Path.Combine(carpetaTemporal, $"{ciudad.Replace(" ", "_")}.txt")

'            ' Verifica si el archivo TXT de la ciudad seleccionada existe
'            If File.Exists(nombreArchivoTxt) Then
'                ' Llama al método para subir el archivo a MEGA
'                SubirArchivoAMega(nombreArchivoTxt)
'            Else
'                MessageBox.Show($"No se encontró el archivo TXT para la ciudad {ciudad}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            End If
'        Next
'    Else
'        MessageBox.Show("Por favor, seleccione al menos una fila antes de intentar subir a MEGA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'    End If
'End Sub

'Private Sub SubirArchivoAMega(rutaArchivo As String)
'    ' Configura tu información de MEGA (reemplaza 'tu_correo_electronico', 'tu_contraseña' y 'ruta_en_mega' con tu información)
'    Dim correoElectronico As String = "dam3.ekaitz.garduno@gmail.com"
'    Dim contrasena As String = "dam3123456789"
'    Dim rutaEnMega As String = "/" ' Reemplaza con la ruta deseada en tu cuenta de MEGA

'    ' Realiza la solicitud de login
'    Dim cliente As New HttpClient()
'    Dim parametrosLogin As New Dictionary(Of String, String) From {
'        {"email", correoElectronico},
'        {"password", contrasena}
'    }
'    Dim contenidoLogin As New FormUrlEncodedContent(parametrosLogin)
'    Dim respuestaLogin = cliente.PostAsync("https://g.api.mega.co.nz/cs?id=CLIENT_ID", contenidoLogin).Result
'    Dim datosLogin = respuestaLogin.Content.ReadAsStringAsync().Result

'    ' Extrae la clave de sesión de la respuesta del login
'    Dim claveSesion As String = datosLogin.Split("""")(3)

'    ' Sube el archivo a MEGA
'    Dim parametrosSubida As New Dictionary(Of String, String) From {
'        {"a", "u"},  ' Operación de subida
'        {"ssl", "1"},
'        {"ms", "true"},
'        {"sid", claveSesion},
'        {"n", Path.GetFileName(rutaArchivo)},  ' Nombre del archivo en MEGA
'        {"t", rutaEnMega}  ' Ruta en MEGA
'    }
'    Dim contenidoSubida As New FormUrlEncodedContent(parametrosSubida)
'    Dim respuestaSubida = cliente.PostAsync("https://g.api.mega.co.nz/cs?id=CLIENT_ID", contenidoSubida).Result
'    Dim datosSubida = respuestaSubida.Content.ReadAsStringAsync().Result

'    ' Analiza la respuesta de subida (puede variar según la respuesta de la API de MEGA)
'    If datosSubida.Contains("""ok"":""") Then
'        MessageBox.Show($"Archivo subido a MEGA con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
'    Else
'        MessageBox.Show($"Error al subir archivo a MEGA: {datosSubida}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'    End If
'End Sub





''ATRIBUTOS DE LA APLICACION

'Private xmlDoc As New XmlDocument()
'Private carpetaTemporal As String

''EL LOAD DE LA APLICACION

'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'    ' Crear una carpeta temporal al iniciar la aplicación
'    carpetaTemporal = Path.Combine(Path.GetTempPath(), "DatosCiudadesTemp")
'    Directory.CreateDirectory(carpetaTemporal)

'    ' Definir la estructura del DataGridView
'    DataGridView1.Columns.Add("Ciudad", "Ciudad")
'    DataGridView1.Columns.Add("Temperatura", "Temperatura ºC")
'    DataGridView1.Columns.Add("Previsión", "Previsión")
'    DataGridView1.Columns.Add("FuerzaViento", "Fuerza del Viento km/h")
'End Sub

''BOTONES DE LA APLICACION

'Private Sub Button_Transformar_y_subir(sender As Object, e As EventArgs) Handles Button2.Click
'    ' Transforma los archivos XML de las filas seleccionadas a formato TXT
'    TransformarXmlATxt()

'    ' Sube los archivos transformados a Dropbox
'    SubirArchivosADropbox()

'    ' Muestra un mensaje de éxito
'    MessageBox.Show("Transformación y subida a Dropbox completa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
'End Sub

'Private Sub Button_Mirar_Prevision_de_una_Ciudad(sender As Object, e As EventArgs) Handles Button3.Click
'    ' Obtenemos la ciudad introducida por el usuario desde el TextBox
'    Dim ciudad As String = tbmirarciudad.Text.Trim()

'    ' Verificamos que la ciudad no esté vacía
'    If String.IsNullOrEmpty(ciudad) Then
'        MessageBox.Show("Por favor, ingrese una ciudad antes de consultar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        Return
'    End If

'    ' Establecemos la dirección URL de la API y los parámetros de consulta con la ciudad introducida
'    Dim apiUrl As String = "http://api.weatherapi.com/v1/current.xml"
'    Dim key As String = "8d7ea9fe295245d7a33122706243001"
'    Dim query As String = $"?key={key}&q={ciudad}&days=7&lang=es"

'    Try
'        ' Cargamos el documento XML desde la API usando el método Load
'        xmlDoc.Load(apiUrl & query)

'        ' Accedemos a los elementos y atributos del documento XML usando el método SelectSingleNode o SelectNodes
'        Dim root As XmlNode = xmlDoc.DocumentElement ' Obtiene el elemento raíz

'        ' Accede a los elementos específicos del XML que deseas mostrar en el DataGridView
'        Dim temperatureNode As XmlNode = root.SelectSingleNode("//temp_c")
'        Dim temperatura As String = temperatureNode.InnerText

'        Dim conditionNode As XmlNode = root.SelectSingleNode("//condition/text")
'        Dim previsión As String = conditionNode.InnerText

'        Dim windNode As XmlNode = root.SelectSingleNode("//wind_kph")
'        Dim fuerzaViento As String = windNode.InnerText

'        ' Agrega una nueva fila al DataGridView con los datos obtenidos
'        DataGridView1.Rows.Add(ciudad, temperatura, previsión, fuerzaViento)

'        ' Guarda los datos en un archivo XML en la carpeta temporal
'        GuardarDatosCiudadEnXML(ciudad, temperatura, previsión, fuerzaViento)
'        tbmirarciudad.Text = ""

'    Catch ex As Exception
'        MessageBox.Show($"Error al obtener la información del tiempo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'    End Try
'End Sub

'Private Sub Button_Eliminar(sender As Object, e As EventArgs) Handles Button4.Click
'    ' Verificamos si hay al menos una fila seleccionada
'    If DataGridView1.SelectedRows.Count > 0 Then
'        ' Mostramos un mensaje de confirmación antes de eliminar las filas
'        Dim result As DialogResult = MessageBox.Show("¿Está seguro de que desea eliminar las filas seleccionadas?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

'        If result = DialogResult.Yes Then
'            ' Eliminamos todas las filas seleccionadas
'            For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
'                DataGridView1.Rows.Remove(selectedRow)
'            Next
'        End If
'    Else
'        MessageBox.Show("Por favor, seleccione al menos una fila antes de intentar eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'    End If
'End Sub

'Private Sub Button_Salir(sender As Object, e As EventArgs) Handles Button1.Click
'    Close()
'End Sub

''TODAS LAS SUB QUE UTILIZAMOS

'Private Sub GuardarDatosCiudadEnXML(ciudad As String, temperatura As String, previsión As String, fuerzaViento As String)
'    ' Genera el nombre del archivo XML utilizando el nombre de la ciudad
'    Dim nombreArchivo As String = Path.Combine(carpetaTemporal, $"{ciudad.Replace(" ", "_")}.xml")

'    ' Crea un nuevo documento XML
'    Dim xmlDocumento As New XmlDocument()

'    ' Crea el nodo raíz
'    Dim raiz As XmlNode = xmlDocumento.CreateElement("DatosCiudad")
'    xmlDocumento.AppendChild(raiz)

'    ' Crea nodos para los datos
'    Dim nodoCiudad As XmlNode = xmlDocumento.CreateElement("Ciudad")
'    nodoCiudad.InnerText = ciudad
'    raiz.AppendChild(nodoCiudad)

'    Dim nodoTemperatura As XmlNode = xmlDocumento.CreateElement("Temperatura")
'    nodoTemperatura.InnerText = temperatura
'    raiz.AppendChild(nodoTemperatura)

'    Dim nodoPrevision As XmlNode = xmlDocumento.CreateElement("Prevision")
'    nodoPrevision.InnerText = previsión
'    raiz.AppendChild(nodoPrevision)

'    Dim nodoFuerzaViento As XmlNode = xmlDocumento.CreateElement("FuerzaViento")
'    nodoFuerzaViento.InnerText = fuerzaViento
'    raiz.AppendChild(nodoFuerzaViento)

'    ' Guarda el documento XML en el archivo
'    xmlDocumento.Save(nombreArchivo)
'End Sub

'Private Sub TransformarXmlATxt()
'    ' Verifica si hay archivos XML en la carpeta temporal
'    Dim archivosXml As String() = Directory.GetFiles(carpetaTemporal, "*.xml")

'    If archivosXml.Length = 0 Then
'        MessageBox.Show("No hay archivos XML para transformar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'        Return
'    End If

'    ' Transforma solo los archivos XML de las filas seleccionadas a formato TXT
'    For Each selectedRow As DataGridViewRow In DataGridView1.SelectedRows
'        ' Genera el nombre del archivo XML utilizando el nombre de la ciudad
'        Dim ciudad As String = selectedRow.Cells("Ciudad").Value.ToString()
'        Dim nombreArchivoXml As String = Path.Combine(carpetaTemporal, $"{ciudad.Replace(" ", "_")}.xml")

'        ' Verifica si el archivo XML de la ciudad seleccionada existe
'        If File.Exists(nombreArchivoXml) Then
'            ' Lee el contenido del archivo XML
'            Dim contenidoXml As String = File.ReadAllText(nombreArchivoXml)

'            ' Transforma y guarda el contenido en un archivo TXT
'            Dim nombreArchivoTxt As String = Path.ChangeExtension(nombreArchivoXml, ".txt")
'            File.WriteAllText(nombreArchivoTxt, contenidoXml)
'        Else
'            MessageBox.Show($"No se encontró el archivo XML para la ciudad {ciudad}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End If
'    Next
'End Sub

'Private Async Sub SubirArchivosADropbox()
'    ' Accede a la carpeta temporal y obtiene la lista de archivos TXT
'    Dim archivosTxt As String() = Directory.GetFiles(carpetaTemporal, "*.txt")

'    If archivosTxt.Length = 0 Then
'        MessageBox.Show("No hay archivos TXT para subir a Dropbox.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
'        Return
'    End If

'    ' Configura la API de Dropbox con tu token de acceso
'    Dim token As String = "sl.Buyh5EVJdU6F-TmnAok3_GqSMr9hjLcq-Va6gC_3XQlA7jmpnn_-rApdecBTxw2XGrsbqTtRTCv_WHnq0cR1l8VhomQESLQconVBixNgK2VdrZAbRb6SytgpBkpHVOJMCG63H09arCmt"
'    Dim client = New DropboxClient(token)

'    ' Sube cada archivo TXT a Dropbox
'    For Each archivoTxt As String In archivosTxt
'        Try
'            ' Lee el contenido del archivo TXT
'            Dim contenidoTxt As String = File.ReadAllText(archivoTxt)

'            ' Obtiene el nombre del archivo desde la ruta completa
'            Dim nombreArchivo As String = Path.GetFileName(archivoTxt)

'            ' Define la ruta en Dropbox donde se almacenará el archivo
'            Dim rutaEnDropbox As String = $"/RetoTemperatura/{nombreArchivo}"


'            ' Sube el archivo a Dropbox
'            Using stream As MemoryStream = New MemoryStream(System.Text.Encoding.UTF8.GetBytes(contenidoTxt))
'                Await client.Files.UploadAsync(rutaEnDropbox, WriteMode.Overwrite.Instance, body:=stream)
'            End Using

'            Console.WriteLine("SE HA SUBIDO")
'        Catch ex As Exception
'            MessageBox.Show($"Error al subir el archivo a Dropbox: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    Next
'End Sub





'Private xmlDoc As New XmlDocument()


'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'    ' Establece la dirección URL de la API y los parámetros de consulta
'    Dim apiUrl As String = "http://api.weatherapi.com/v1/current.xml"
'    Dim key As String = ""
'    Dim query As String = "?key=8d7ea9fe295245d7a33122706243001&q=Sestao&days=7&lang=es"

'    ' Carga el documento XML desde la API usando el método Load
'    xmlDoc.Load(apiUrl & query)

'    ' Accede a los elementos y atributos del documento XML usando el método SelectSingleNode o SelectNodes
'    Dim root As System.Xml.XmlNode = xmlDoc.DocumentElement ' Obtiene el elemento raíz
'    Dim elements As System.Xml.XmlNodeList = root.ChildNodes ' Obtiene los elementos hijos del elemento raíz
'    Dim attribute As System.Xml.XmlAttribute = root.Attributes("id") ' Obtiene el atributo id del elemento raíz

'    ' Haz algo con el documento XML, por ejemplo, mostrarlo en la consola
'    xmlDoc.Save("../Reto2Temperatura.xml")

'End Sub

''TODOS LOS BOTONES QUE HAY Y SUS FUNCIONALIDADES

'Private Sub Button_Transformar_y_subir(sender As Object, e As EventArgs) Handles Button2.Click

'End Sub

'Private Sub Button_Mirar_Prevision_de_una_Ciudad(sender As Object, e As EventArgs) Handles Button3.Click

'End Sub
'Private Sub Button_Salir(sender As Object, e As EventArgs) Handles Button1.Click
'    Close()
'End Sub

