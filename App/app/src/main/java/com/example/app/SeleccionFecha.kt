package com.example.app

import android.app.DatePickerDialog
import android.app.Dialog
import android.content.Context
import android.icu.util.Calendar
import android.os.Build
import android.os.Bundle
import android.widget.DatePicker
import androidx.annotation.RequiresApi
import androidx.fragment.app.DialogFragment
/*
class SeleccionFecha(val listener: (dia:Int, mes:Int, anO:Int)-> Unit): DialogFragment(),
        DatePickerDialog.OnDateSetListener {

    override fun onDateSet(view: DatePicker?, year: Int, month: Int, dayOfMonth: Int) {
        listener(dayOfMonth, month, year)
    }

    @RequiresApi(Build.VERSION_CODES.N)
    override fun onCreateDialog(savedInstanceState: Bundle?): Dialog {

        val calendario:java.util.Calendar = java.util.Calendar.getInstance()
        val dia = calendario.get(java.util.Calendar.DAY_OF_MONTH)
        val mes = calendario.get(java.util.Calendar.MONTH)
        val ano = calendario.get(java.util.Calendar.YEAR)
        val picker = DatePickerDialog( activity as Context, this, ano, mes, dia)

        picker.datePicker.maxDate = calendario.timeInMillis
        return picker
    }
}

 */