package com.example.app

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import kotlinx.android.synthetic.main.menu.*

class Menu: AppCompatActivity() {

    val comida: List<Platos> = listOf(
        Platos(0,"Ensalada", 100,"Hola soy una ensalada", 500,"comida", 0,0,0, "Bueno") ,
        Platos(1,"Frijoles", 111,"Frijolitos", 600,"comida", 0,0,0, "Bueno"),
        Platos(2,"Arroz", 222,"Arrozzzz", 1500,"comida", 0,0,0, "Bueno"),
        Platos(3,"Lasagna", 333,"Una Lasagna", 800,"comida", 0,0,0, "Bueno"),
        Platos(4,"Macarrones", 444,"Unos macarrones", 0,"comida", 0,0,0, "Bueno"),
        Platos(5,"Pollo", 444,"Un Pollo", 0,"comida", 0,0,0, "Bueno"),
        Platos(6,"Pinto", 444,"Un pinto", 0,"comida", 0,0,0, "Bueno"),
        Platos(7,"Huevo", 666,"Un huevo", 0,"comida", 0,0,0, "Bueno"),
        Platos(8,"Chorizo", 677,"Un chorizo", 0,"comida", 0,0,0, "Bueno"),
        Platos(9,"Frutas", 445,"Unas frutas", 0,"comida", 0,0,0, "Bueno")
    )

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.menu)
        IncializarPlatillos()
    }

    fun IncializarPlatillos(){
        recycleviewplatillos.layoutManager = LinearLayoutManager(this)
        val adapter = ComAdapter(comida, this)
        recycleviewplatillos.adapter = adapter
    }
}