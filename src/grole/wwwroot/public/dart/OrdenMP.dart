library OrdenMP;

import 'package:web_ui/web_ui.dart';
import 'dart:json' as json;
import 'package:web_ui/observe/observable.dart' as __observe;


@observable
class OrdenMP extends Observable {
  int __$id;
  int get id {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'id');
    }
    return __$id;
  }
  set id(int value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'id',
          __$id, value);
    }
    __$id = value;
  }
  int __$id_orden;
  int get id_orden {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'id_orden');
    }
    return __$id_orden;
  }
  set id_orden(int value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'id_orden',
          __$id_orden, value);
    }
    __$id_orden = value;
  }
  String __$producto;
  String get producto {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'producto');
    }
    return __$producto;
  }
  set producto(String value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'producto',
          __$producto, value);
    }
    __$producto = value;
  }
  String __$descripcion;
  String get descripcion {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'descripcion');
    }
    return __$descripcion;
  }
  set descripcion(String value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'descripcion',
          __$descripcion, value);
    }
    __$descripcion = value;
  }
  double __$kilos = 0.0;
  double get kilos {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'kilos');
    }
    return __$kilos;
  }
  set kilos(double value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'kilos',
          __$kilos, value);
    }
    __$kilos = value;
  }
  double __$producido;
  double get producido {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'producido');
    }
    return __$producido;
  }
  set producido(double value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'producido',
          __$producido, value);
    }
    __$producido = value;
  }
  double __$rendimiento;
  double get rendimiento {
    if (__observe.observeReads) {
      __observe.notifyRead(this, __observe.ChangeRecord.FIELD, 'rendimiento');
    }
    return __$rendimiento;
  }
  set rendimiento(double value) {
    if (__observe.hasObservers(this)) {
      __observe.notifyChange(this, __observe.ChangeRecord.FIELD, 'rendimiento',
          __$rendimiento, value);
    }
    __$rendimiento = value;
  }
  
  String toJSON(){
    Map p_map = new Map();
    p_map['id']          = this.id;
    p_map['id_orden']    = this.id_orden;
    p_map['producto']    = this.producto;
    p_map['descripcion'] = this.descripcion;
    p_map['kilos']       = this.kilos;
    p_map['producido']   = this.producido;
    p_map['rendimiento'] = this.rendimiento;
    
    return json.stringify(p_map);
  }
  
  OrdenMP();
  
  OrdenMP.FromMap(Map AMap){
    
    this.id          = AMap['id'];
    this.id_orden    = AMap['id_orden'];
    this.producto    = AMap['producto'];
    this.descripcion = AMap['descripcion'];
    this.kilos       = AMap['kilos'];
    
    try{
      this.producido   = AMap['producido'];
      this.rendimiento = AMap['rendimiento'];
    }
    catch(error){
      this.producido   = 0.0;
      this.rendimiento = 0.0;
    }
   
  }
}


//# sourceMappingURL=OrdenMP.dart.map