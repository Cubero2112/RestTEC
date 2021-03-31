package com.example.app

import android.app.Activity
import android.app.AlertDialog

class Cargar (val visualizar: Activity) {

    private lateinit var isdialog:AlertDialog

    fun ComenzarCarga (){

        val infalter = visualizar.layoutInflater
        val dialogView = infalter.inflate(R.layout.cargar,null)
        val builder = AlertDialog.Builder(visualizar)

        builder.setView(dialogView)
        builder.setCancelable(false)
        isdialog = builder.create()
        isdialog.show()
    }

    fun isDissmis(){
        isdialog.dismiss()
    }


}