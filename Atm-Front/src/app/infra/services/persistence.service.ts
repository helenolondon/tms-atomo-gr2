/**
 * Autor: Heleno Sales Mesquita
 * Data : 09/02/2020
 * 
 * Proxy de persistencia de no browser
 */

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PersistenceService {

  /**
   * Armazena uma string no local store 
   * @param key chave de identificação
   * @param value valor a ser armazenado
   */
  public setItem(key: string, value: string){
    localStorage.setItem(key, value)
  }

   /**
   * Armazena um valor lógico no local store 
   * @param key chave de identificação
   * @param value valor a ser armazenado
   */
  public setItemAsBoolean(key: string, value: Boolean){
    localStorage.setItem(key, String(value))
  }

   /**
   * Armazena um objeto no local store 
   * @param key chave de identificação
   * @param value objeto a ser armazenado
   */
  public setObject(key: string, data: object){
    localStorage.setItem(key, JSON.stringify(data));
  }

  /**
   * Retorna o item em formato string
   *  @param key chave do item no store
   */
  public getItemAsString(key: string){
    return localStorage.getItem(key)
  }

  /**
   * Retorna o item como objeto
   * @param key chave do item no store
   */
  public getItemObject(key: string): object{
    return JSON.parse(localStorage.getItem(key))
  }
  
  /**
   * Retorna o item como valor lógico
   * @param key chave do item no store
   */
  public getItemAsBoolean(key: string): Boolean{
    return JSON.parse(localStorage.getItem(key))
  }

  /**
   * Remove o item do store
   * @param key chave do item no store
   */
  public removeItem(key: string){
    localStorage.removeItem(key);
  }

  /**
   * Retorna a quantidade de itens no store
   */
  public count(){
    return localStorage.length;
  }

  /**
   * Remove todos os itens do store
   */
  public clear(){
    localStorage.clear();
  }

  constructor() { }
}
