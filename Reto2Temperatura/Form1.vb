Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Xml
Imports AngleSharp.Dom
Imports CG.Web.MegaApiClient
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
    Dim megaUsername As String = "dam3.ibai.fuentes@gmail.com"
    Dim megaPassword As String = "ibi20033"

    Dim URLarchivoXML As String

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

        CargarArchivosXml()



    End Sub

    Private Sub CargarArchivosXml()
        ' Obtener la lista de archivos XML en la carpeta
        Try
            URLarchivoXML = Directory.GetFiles(carpetaXml, "*.xml")(0)

            ' Cargar el archivo XML
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(URLarchivoXML)

            ' Obtener información del XML para añadir al DataGridView
            Dim ciudadNodes As XmlNodeList = xmlDoc.SelectNodes("/Ciudades/Ciudad")
            If ciudadNodes.Count > 0 Then
                For Each ciudadNode As XmlNode In ciudadNodes
                    Dim ciudad As String = ciudadNode.Attributes("Nombre")?.Value
                    Dim temperatura As String = ciudadNode.SelectSingleNode("Temperatura")?.InnerText
                    Dim prevision As String = ciudadNode.SelectSingleNode("Prevision")?.InnerText
                    Dim fuerzaViento As String = ciudadNode.SelectSingleNode("FuerzaViento")?.InnerText

                    ' Añadir la información al DataGridView
                    DataGridView1.Rows.Add(ciudad, temperatura, prevision, fuerzaViento)

                    ' Mensaje de éxito
                    Console.WriteLine($"Se cargó '{URLarchivoXML}' correctamente.")
                Next
            End If

        Catch xmlEx As XmlException
            ' Manejar errores relacionados con XML
            Console.WriteLine($"Error al cargar XML en '{URLarchivoXML}': {xmlEx.Message}")

        Catch ex As Exception
            ' Manejar otros errores
            Console.WriteLine($"Error al procesar '{URLarchivoXML}': {ex.Message}")
        End Try
    End Sub

    'TODOS LOS BOTONES QUE HAY Y SUS FUNCIONALIDADES

    Private Sub Button_Transformar_y_subir(sender As Object, e As EventArgs) Handles btEnviarSeleccionados.Click


        lblCiudadActual.BackColor = Color.FromArgb(0, 0, 0, 0) ' Transparente
        lblTempActual.BackColor = Color.FromArgb(0, 0, 0, 0)  ' Transparente
        lblCiudadActual.ForeColor = Color.White
        lblTempActual.ForeColor = Color.White
        paneltexto.BackColor = Color.FromArgb(0, 0, 0, 0)
        panelPrevision.BackColor = Color.FromArgb(0, 0, 0, 0)

        publicarInforme(carpetaXml, carpetaTxt)



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
                    Dim ciudadEliminar As String = selectedRow.Cells("Ciudad").Value.ToString()

                    ' Eliminar la fila del DataGridView
                    DataGridView1.Rows.Remove(selectedRow)

                    ' Eliminar la información del archivo XML
                    EliminarCiudadDeXML(ciudadEliminar)
                Next
            End If
        Else
            MessageBox.Show("Por favor, seleccione al menos una fila antes de intentar eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub EliminarCiudadDeXML(ciudadEliminar As String)

        xmlDoc.Load(URLarchivoXML)

        ' Buscar el nodo de la ciudad a eliminar
        Dim nodoCiudadEliminar As XmlNode = xmlDoc.SelectSingleNode($"/Ciudades/Ciudad[@Nombre='{ciudadEliminar}']")

        ' Si se encuentra el nodo, eliminarlo
        If nodoCiudadEliminar IsNot Nothing Then
            nodoCiudadEliminar.ParentNode.RemoveChild(nodoCiudadEliminar)
            ' Guardar el XML actualizado
            xmlDoc.Save(URLarchivoXML)
        End If
    End Sub


    Private Sub Button_Salir(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub
    Private Sub btAnadirEnInforme_Click(sender As Object, e As EventArgs) Handles btAnadirEnInforme.Click
        anadirTiempoEnInforme()
    End Sub

    '---------------------------------------------------------- FUNCIONALIDADES --------------------------------------------------------------------

    Private Sub PublicarInforme(ByVal carpetaXml As String, ByVal carpetaTxt As String)
        ' Obtener la lista de archivos XML en la carpeta
        Dim archivosXml As String() = Directory.GetFiles(carpetaXml, "*.xml")

        ' Crear una lista para almacenar los documentos XML fusionados
        Dim documentosFusionados As New List(Of XmlDocument)()

        ' Recorrer cada archivo XML
        For Each archivoXml As String In archivosXml
            Try
                ' Cargar el archivo XML
                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(archivoXml)

                ' Agregar el documento XML a la lista
                documentosFusionados.Add(xmlDoc)

                ' Mensaje de éxito
                Console.WriteLine($"Se cargó '{archivoXml}' correctamente.")

            Catch xmlEx As XmlException
                ' Manejar errores relacionados con XML
                Console.WriteLine($"Error al cargar XML en '{archivoXml}': {xmlEx.Message}")

            Catch ex As Exception
                ' Manejar otros errores
                Console.WriteLine($"Error al procesar '{archivoXml}': {ex.Message}")
            End Try
        Next

        ' Fusionar todos los documentos XML en uno solo
        Dim xmlFusionado As New XmlDocument()

        ' Crear el elemento raíz del documento fusionado
        Dim raizFusionada As XmlElement = xmlFusionado.CreateElement("RaizFusionada")
        xmlFusionado.AppendChild(raizFusionada)

        ' Agregar cada documento a la raíz fusionada
        For Each documento In documentosFusionados
            Dim nodoImportado As XmlNode = xmlFusionado.ImportNode(documento.DocumentElement, True)
            raizFusionada.AppendChild(nodoImportado)
        Next

        ' Obtener la fecha actual para incluir en el nombre del archivo
        Dim fechaActual As String = DateTime.Now.ToString("yyyyMMdd")

        ' Guardar el documento fusionado en un nuevo archivo XML con nombre 'Informe+FechaActual.xml'
        Dim archivoXmlFusionado As String = Path.Combine(carpetaTxt, $"Informe_{fechaActual}.xml")
        xmlFusionado.Save(archivoXmlFusionado)

        ' Convertir el archivo XML fusionado a un archivo de texto con nombre 'Informe+FechaActual.txt'
        Dim archivoTxt As String = Path.Combine(carpetaTxt, $"Informe_{fechaActual}.txt")
        Using writer As New StreamWriter(archivoTxt)
            ' Escribir el contenido XML en el archivo de texto
            writer.Write(xmlFusionado.OuterXml)
        End Using

        ' Publicar en Mega
        PublicarMega(archivoTxt)

        ' Mensaje de éxito
        Console.WriteLine($"Se fusionaron los archivos XML y se creó '{archivoXmlFusionado}' y '{archivoTxt}'")
        MessageBox.Show("Proceso de conversión y fusión completado.")
    End Sub


    Private Sub anadirTiempoEnInforme()
        ' Obtener la fecha actual para incluir en el nombre del archivo
        Dim fechaActual As String = DateTime.Now.ToString("yyyyMMdd")

        ' Crear el nombre del archivo XML con nombre 'Informe+FechaActual.xml'
        Dim archivoXml As String = Path.Combine(carpetaXml, $"Informe_{fechaActual}.xml")

        ' Verificar si el archivo XML ya existe
        Dim nuevoXmlDoc As New XmlDocument()

        If File.Exists(archivoXml) Then
            ' Si el archivo existe, cargar el documento existente
            nuevoXmlDoc.Load(archivoXml)

            ' Buscar el nodo Ciudad con el mismo nombre
            Dim nodoCiudadExistente As XmlNode = nuevoXmlDoc.SelectSingleNode($"//Ciudad[@Nombre='{ciudadActual}']")

            If nodoCiudadExistente IsNot Nothing Then
                ' Si la ciudad ya existe, sobrescribir la información
                nodoCiudadExistente.SelectSingleNode("Temperatura").InnerText = temperaturaActual
                nodoCiudadExistente.SelectSingleNode("Prevision").InnerText = previsionActual
                nodoCiudadExistente.SelectSingleNode("FuerzaViento").InnerText = fuerzaVientoActual
            Else
                ' Si la ciudad no existe, crear un nuevo nodo Ciudad
                Dim nuevoNodoCiudad As XmlElement = nuevoXmlDoc.CreateElement("Ciudad")
                nuevoNodoCiudad.SetAttribute("Nombre", ciudadActual)
                nuevoXmlDoc.DocumentElement.AppendChild(nuevoNodoCiudad)

                Dim nodoTemperatura As XmlElement = nuevoXmlDoc.CreateElement("Temperatura")
                nodoTemperatura.InnerText = temperaturaActual
                nuevoNodoCiudad.AppendChild(nodoTemperatura)

                Dim nodoPrevision As XmlElement = nuevoXmlDoc.CreateElement("Prevision")
                nodoPrevision.InnerText = previsionActual
                nuevoNodoCiudad.AppendChild(nodoPrevision)

                Dim nodoFuerzaViento As XmlElement = nuevoXmlDoc.CreateElement("FuerzaViento")
                nodoFuerzaViento.InnerText = fuerzaVientoActual
                nuevoNodoCiudad.AppendChild(nodoFuerzaViento)
            End If
        Else
            ' Si el archivo no existe, crear un nuevo documento XML con el elemento raíz
            Dim raizNuevoDocumento As XmlElement = nuevoXmlDoc.CreateElement("Ciudades")
            nuevoXmlDoc.AppendChild(raizNuevoDocumento)

            ' Crear un nuevo nodo Ciudad
            Dim nuevoNodoCiudad As XmlElement = nuevoXmlDoc.CreateElement("Ciudad")
            nuevoNodoCiudad.SetAttribute("Nombre", ciudadActual)
            raizNuevoDocumento.AppendChild(nuevoNodoCiudad)

            Dim nodoTemperatura As XmlElement = nuevoXmlDoc.CreateElement("Temperatura")
            nodoTemperatura.InnerText = temperaturaActual
            nuevoNodoCiudad.AppendChild(nodoTemperatura)

            Dim nodoPrevision As XmlElement = nuevoXmlDoc.CreateElement("Prevision")
            nodoPrevision.InnerText = previsionActual
            nuevoNodoCiudad.AppendChild(nodoPrevision)

            Dim nodoFuerzaViento As XmlElement = nuevoXmlDoc.CreateElement("FuerzaViento")
            nodoFuerzaViento.InnerText = fuerzaVientoActual
            nuevoNodoCiudad.AppendChild(nodoFuerzaViento)
        End If

        ' Guardar el documento XML actualizado
        nuevoXmlDoc.Save(archivoXml)

        ' Añadir la información al DataGridView
        DataGridView1.Rows.Clear()
        CargarArchivosXml()


        ' Mensaje de éxito
        Console.WriteLine($"Se añadió la información al archivo '{archivoXml}' y al DataGridView.")
    End Sub



    Private Sub PublicarMega(archivo As String)
        Dim carpetaTxt As String = archivo

        Dim megaApiClient As New MegaApiClient()

        Try
            megaApiClient.Login(megaUsername, megaPassword)

            ' Obtener el nombre del archivo sin la ruta completa
            Dim fileName As String = Path.GetFileName(archivo)

            ' Obtener todos los nodos en la carpeta de Mega
            Dim megaNodes = megaApiClient.GetNodes()

            ' Verificar si ya existe un archivo con el mismo nombre
            Dim existingNode = megaNodes.FirstOrDefault(Function(node) node.Name = fileName)

            If existingNode IsNot Nothing Then
                megaApiClient.Delete(existingNode, True)
            End If

            ' Subir el nuevo archivo
            SubirArchivoAMega(megaApiClient, carpetaTxt)

            megaApiClient.Logout()
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub

    Sub SubirArchivoAMega(ByVal megaApiClient As MegaApiClient, ByVal filePath As String)
        Using fileStream As New FileStream(filePath, FileMode.Open)
            Dim fileName As String = Path.GetFileName(filePath)
            Dim parentNode = megaApiClient.GetNodes().First()
            megaApiClient.Upload(fileStream, fileName, parentNode)
        End Using

        MessageBox.Show("Archivo subido exitosamente a Mega.")
    End Sub

End Class
