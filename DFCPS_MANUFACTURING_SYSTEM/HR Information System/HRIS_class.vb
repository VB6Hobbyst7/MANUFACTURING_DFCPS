Imports System.Data.SqlClient

Public Class HRIS_class
    Public Function get_labor_cost(ByVal empid, ByVal rHours, ByVal latemin, ByVal otHours, ByVal rdrHours, ByVal rhHours, ByVal nwhHours) As Decimal
        Dim laborcost As Decimal = 0
        Try
            Dim dt As New DataTable
            Dim rate As Decimal
            Dim salaryType As String
            Dim dayFactor As Integer
            checkConn()
            Dim cmd As New SqlCommand("select rate,payMethod,grade from tblEmployeesInfo where empID = '" & empid & "'", conn)
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            rate = dt.Rows(0).Item(0)
            salaryType = dt.Rows(0).Item(1)
            salaryType = dt.Rows(0).Item(2)
            If salaryType = "Daily" Then
                laborcost += (rate / 8) * rhHours
                laborcost += ((rate / 8) * otHours) * 1.25
                laborcost += ((rate / 8) * rdrHours) * 1.3
                laborcost += ((rate / 8) * rhHours) + rate
                laborcost += ((rate / 8) * nwhHours) * 1.3
                laborcost -= ((rate / 8 / 60) * latemin)
            ElseIf salaryType = "Monthly" Then
                laborcost += (rate / dayFactor / 8) * rhHours
                laborcost += ((rate / dayFactor / 8) * otHours) * 1.25
                laborcost += ((rate / dayFactor / 8) * rdrHours) * 1.3
                laborcost += ((rate / dayFactor / 8) * rhHours) + (rate / dayFactor)
                laborcost += ((rate / dayFactor / 8) * nwhHours) * 1.3
                laborcost -= ((rate / dayFactor / 8 / 60) * latemin)
            End If
            Return laborcost
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return laborcost
    End Function
End Class
