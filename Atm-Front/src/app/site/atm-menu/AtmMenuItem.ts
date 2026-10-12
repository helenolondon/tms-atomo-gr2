import { AtmMenuClickEvent } from './AtmMenuClickEvent';

export class AtmMenuItem{
	private _itens: AtmMenuItem[] = [];

	id?: string;
	parent?: AtmMenuItem;
	titulo?: string;
	aberto?: boolean = false;
	selecionado?: boolean = false;
	link?: string;
	onClick?: AtmMenuClickEvent;
	
	constructor (titulo: string, onClick?: AtmMenuClickEvent, link?: string, id?: string){
		this.id = id;
		this.titulo = titulo;
		this._itens = [];
		this.onClick = onClick;
		this.link = link;
	}

	itens(): AtmMenuItem[] { return this._itens };

	AddItens(subMenuItens: AtmMenuItem[])
	{
		if(!subMenuItens || subMenuItens.length < 1)
		{
			return;
		}

		subMenuItens.forEach(mn => {
			mn.parent = this;
			this._itens.push(mn);
		});
	}

	AddItem(menuIten: AtmMenuItem): AtmMenuItem
	{
		if(!menuIten)
		{
			return;
		}
		
		menuIten.parent = this;
		this._itens.push(menuIten);
		
		return this;
	}
}