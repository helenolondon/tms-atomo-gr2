import { AtmMenuItem } from './AtmMenuItem';

export class AtmMenuClickEventArgs {
	menu: AtmMenuItem;
	 constructor(menuItem: AtmMenuItem){
		this.menu = menuItem;
	 }
}

export type AtmMenuClickEvent = (menu: AtmMenuClickEventArgs) => void;
