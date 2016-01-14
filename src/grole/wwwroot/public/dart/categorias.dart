import 'dart:html';
import 'dart:json' as json;
import 'models/categoria.dart';
import 'views/categoria_/lista.dart';
import  'dart:collection';

ListaCategoria_ pListaCategoria;
ViewListaCategoria_ pViewListaCategoria;

class MiClase1 extends HashMap<String, int>{
    
}

void llenaLista(){
    
    var pUrl = "http://localhost:8031/rest/categorias/obtener_lista";
  
    HttpRequest.getString(pUrl).then((String Data){
    
        List pLista = json.parse(Data);
      
        for (Map item in pLista){
            pListaCategoria.add(new Categoria_.fromMap(item));
        }
    });
}

void main() {
  
    pViewListaCategoria = new ViewListaCategoria_(document.query("#t1"));
    pListaCategoria = new ListaCategoria_(pViewListaCategoria);
    
    llenaLista();
    
}