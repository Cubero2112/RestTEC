package com.example.app

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import kotlinx.android.synthetic.main.menu.*

class Menu: AppCompatActivity() {

    val comida: List<Platos> = listOf(
            Platos(0,"Ensalada", 100,"Hola soy una ensalada", 200,"comida", 3,9,3, "Bueno") ,
            Platos(1,"Frijoles", 400,"Frijolitos", 300,"comida", 5,4,8, "Bueno"),
            Platos(2,"Arroz", 222,"Arrozzzz", 600,"comida", 4,7,8, "Bueno"),
            Platos(3,"Lasagna", 333,"Una Lasagna", 800,"comida", 4,6,0, "Bueno"),
            Platos(4,"Macarrones", 444,"Unos macarrones", 420,"comida", 4,0,0, "Bueno"),
            Platos(5,"Pollo", 600,"Un Pollo", 380,"carne", 4,8,7, "Bueno"),
            Platos(6,"Pinto", 444,"Un pinto", 680,"comida", 4,7,6, "Bueno"),
            Platos(7,"Huevo", 666,"Un huevo", 450,"comida", 4,3,7, "Bueno"),
            Platos(8,"Chorizo", 677,"Un chorizo", 500,"comida", 3,2,6, "Bueno"),
            Platos(9,"Frutas", 445,"Unas frutas", 300,"comida", 4,4,2, "Bueno")
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