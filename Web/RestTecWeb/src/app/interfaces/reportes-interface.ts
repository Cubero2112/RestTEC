import {PlatilloInterface} from "src/app/interfaces/platillos-interface";
import {UsuarioInterface} from "src/app/interfaces/usuario-interface";

export interface ReportesInterface{

	PlatillosMasVendidos?: PlatilloInterface[],
	PlatillosConMasGanancias?: PlatilloInterface[],
	PlatillosMejorFeedBack?: PlatilloInterface[],
	ClientesMasFieles?: any[]
}