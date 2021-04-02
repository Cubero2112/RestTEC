import {PlatilloInterface} from "./platillos-interface"

export interface PedidoInterface {

	Chef?: string,
	Platillos?: PlatilloInterface[],
	TiempoPreparacionPromedio?: number,
	EstaListo?: boolean,
	Orden?: number,
	Init?: number,
	Finish?: number,
	TiempoPreparacionReal?: number

}


