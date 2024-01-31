Imports System.IO
Imports System.Net
Imports System.Xml

Public Class Form1

    Private xmlDoc As New XmlDocument()


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Establece la dirección URL de la API y los parámetros de consulta
        Dim apiUrl As String = "http://api.weatherapi.com/v1/current.xml"
        Dim key As String = ""

        Dim query As String = "?key=8d7ea9fe295245d7a33122706243001&q=Sestao&days=7&lang=es"

        ' Carga el documento XML desde la API usando el método Load
        xmlDoc.Load(apiUrl & query)

        ' Accede a los elementos y atributos del documento XML usando el método SelectSingleNode o SelectNodes
        Dim root As System.Xml.XmlNode = xmlDoc.DocumentElement ' Obtiene el elemento raíz
        Dim elements As System.Xml.XmlNodeList = root.ChildNodes ' Obtiene los elementos hijos del elemento raíz
        Dim attribute As System.Xml.XmlAttribute = root.Attributes("id") ' Obtiene el atributo id del elemento raíz

        ' Haz algo con el documento XML, por ejemplo, mostrarlo en la consola
        xmlDoc.Save("../Reto2Temperatura.xml")

    End Sub
    ' Crea un objeto XmlDocument para cargar el documento XML desde la API


End Class