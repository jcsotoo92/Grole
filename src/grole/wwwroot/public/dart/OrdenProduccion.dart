library LibOrdenProduccion;
import 'package:web_ui/web_ui.dart';
import 'dart:json' as json;
import 'package:intl/intl.dart';

class OrdenProduccion{
  int id;
  String fecha;
  int id_usuario;
  int id_usuario_embarques;
  int id_tipo;
  int id_programacion;
  String id_salida;
  String descripcion;
  double kilos_produccion;
  double rendimiento;
  String estado;
  String lote_produccion;
  
  String ToJson(){
    Map p_map = new Map();

    p_map['id']                   = this.id;
    p_map['fecha']                = this.fecha;
    p_map['id_usuario']           = this.id_usuario;
    p_map['id_usuario_embarques'] = this.id_usuario_embarques;
    p_map['id_tipo']              = this.id_tipo;
    p_map['descripcion']          = this.descripcion;
    p_map['kilos_produccion']     = this.kilos_produccion;
    p_map['rendimiento']          = this.rendimiento;
    p_map['estado']               = this.estado;
    p_map['lote_produccion']      = this.lote_produccion;
    
    return json.stringify(p_map);
  }

  OrdenProduccion();
  
  OrdenProduccion.FromJSON(String AJSON){
    
    var p_lista = json.parse(AJSON);
    
    this.id = p_lista['id'];
    
    String pFecha = p_lista['fecha'];
    pFecha = pFecha.substring(0, 10);
    
    DateFormat pDateFormat = new DateFormat("dd/MM/yyyy");
    
    pFecha = pDateFormat.format(DateTime.parse(pFecha));
    
    this.fecha                = pFecha;
    this.id_usuario           = p_lista['id_usuario'];
    this.id_usuario_embarques = p_lista['id_usuario_embarques'];
    this.id_tipo              = p_lista['id_tipo'];
    this.id_programacion      = p_lista['id_programacion'];
    this.descripcion          = p_lista['descripcion'];
    this.kilos_produccion     = p_lista['kilos_producccion'];
    this.rendimiento          = p_lista['rendimiento'];
    this.estado               = p_lista['estado'];
    this.lote_produccion      = p_lista['lote_produccion'].toString();
    this.id_salida            = p_lista['id_salida_embarques'].toString();
   
  }
}

class Produccion{
    String clave_producto;
    String nombre_producto;
    int cajas;
    double kilos;
    
    Produccion();
    
    Produccion.fromMap(Map AMapa){
        this.clave_producto  = AMapa["producto"];
        this.nombre_producto = AMapa["descripcion"];
        this.cajas           = AMapa["cajas"];
        this.kilos           = AMapa["kilos"].toDouble();
    }
}