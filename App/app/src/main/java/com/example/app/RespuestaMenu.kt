package com.example.app

import com.google.gson.annotations.SerializedName

data class RespuestaMenu (
    @SerializedName("Codigo") var identificador: String,
    @SerializedName("Comida") var platillo_nombre: String,
    @SerializedName("Descripcion") var des: String,
    @SerializedName("Tiempo") var tiempo_prep: String
)
