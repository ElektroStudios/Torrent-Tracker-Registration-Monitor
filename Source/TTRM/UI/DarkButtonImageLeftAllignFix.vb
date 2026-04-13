#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel

Imports DarkUI.Controls

#End Region

''' <summary>
''' A subclass of <see cref="DarkButton"/> that "fixes" the image alignment issue when 
''' drawing images to the left or above the text.
''' <para></para>
''' The original <see cref="DarkButton"/> has design flaws where assigning an image 
''' shifts the text too far to the right, sometimes outside the visible bounds of the control. 
''' This subclass manually draws the image aligned to the left or above the text.
''' <para></para>
''' Only the <see cref="TextImageRelation.ImageBeforeText"/> and 
''' <see cref="TextImageRelation.ImageAboveText"/> values are supported.
''' <para></para>
''' Note that this fix only aligns the image; the text alignment remains unchanged.
''' </summary>
Public Class DarkButtonImageAllignFix : Inherits DarkButton

#Region " Public Properties "

    ''' <summary>
    ''' Gets or sets the image that will be resized and drawn manually on the button.
    ''' </summary>
    <Browsable(True)>
    <TypeConverter(GetType(ImageConverter))>
    Public Property ResizedImage As Image
        <DebuggerStepThrough>
        Get
            Return Me._resizedImage
        End Get
        <DebuggerStepThrough>
        Set(value As Image)
            Me._resizedImage = value
            Me.Invalidate()
        End Set
    End Property
    ''' <summary>
    ''' ( Backing field of <see cref="ResizedImage"/> property. )
    ''' <para></para>
    ''' The image that will be resized and drawn manually on the button.
    ''' </summary>
    Private _resizedImage As Image

#End Region

#Region " Event Invokers "

    ''' <summary>
    ''' Raises the <see cref="DarkButtonImageAllignFix.Paint"/> event.
    ''' </summary>
    ''' 
    ''' <param name="e">
    ''' The <see cref="PaintEventArgs"/> instance containing the event data.
    ''' </param>
    ''' 
    ''' <exception cref="NotImplementedException">
    ''' {ResizedImage} logic not implemented for {TextImageRelation}. Affected control: {Name}
    ''' </exception>
    <DebuggerStepperBoundary>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        If Me.DesignMode OrElse
           (Me._resizedImage Is Nothing) OrElse
           (Me.Image IsNot Nothing) Then

            Exit Sub
        End If

        If Me.TextImageRelation = TextImageRelation.ImageBeforeText Then
            Dim scale As Double =
                Math.Min((Me.ClientRectangle.Width / 1.5) / Me._resizedImage.Width,
                         (Me.ClientRectangle.Height / 1.5) / Me._resizedImage.Height)

            Dim newWidth As Integer = CInt(Me._resizedImage.Width * scale)
            Dim newHeight As Integer = CInt(Me._resizedImage.Height * scale)

            Dim g As Graphics = e.Graphics
            Dim imgX As Integer = 8
            Dim imgY As Integer = (Me.ClientRectangle.Height - newHeight) \ 2
            With g
                .InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                .SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                .PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                .CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                .DrawImage(Me._resizedImage, imgX, imgY, newWidth, newHeight)
            End With

        ElseIf Me.TextImageRelation = TextImageRelation.ImageAboveText Then
            Dim scale As Double =
                Math.Min((Me.ClientRectangle.Width / 2.0) / Me._resizedImage.Width,
                         (Me.ClientRectangle.Height / 2.0) / Me._resizedImage.Height)

            Dim newWidth As Integer = CInt(Me._resizedImage.Width * scale)
            Dim newHeight As Integer = CInt(Me._resizedImage.Height * scale)

            Dim resizedImage As New Bitmap(newWidth, newHeight)
            Using g As Graphics = Graphics.FromImage(resizedImage)
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                g.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality
                g.CompositingQuality = Drawing2D.CompositingQuality.HighQuality
                g.DrawImage(Me._resizedImage, 0, 2, newWidth, newHeight)
            End Using
            Me.Image = resizedImage
        Else
            Throw New NotImplementedException($"{NameOf(Me.ResizedImage)} logic not implemented for {Me.TextImageRelation}. Affected control: {Me.Name}")

        End If
    End Sub

#End Region

End Class