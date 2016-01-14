library VistaCategoria_;

import 'dart:html';
import '../../models/categoria.dart';

class ViewListaCategoria_{
  TableElement _tableLista;
  ListaCategoria_ _ListaCategoria_;

  ViewListaCategoria_(Element AElement){
    this._tableLista = AElement;
    this._tableLista.appendHtml('<tbody>');

    ButtonElement pBtnAgregar = this._tableLista.query('#btnAgregar');

    if (pBtnAgregar != null){
      pBtnAgregar.onClick.listen((_){

      int pId = 0;
      bool pPuedeAgregar = true;
      String pCategoria = this._tableLista.query('#inpCategoria').value;
      String pActivo = this._tableLista.query('#inpActivo').value;
      if (pPuedeAgregar){
        this._ListaCategoria_.insert(new Categoria_(pId, pCategoria, pActivo, true));
          this._tableLista.query('#inpId').value = '';
          this._tableLista.query('#inpCategoria').value = '';
          this._tableLista.query('#inpActivo').value = 'Si';
          this._tableLista.query('#inpCategoria').focus();
        }
      });

    }
  }

  setListaCategoria_(ListaCategoria_ AListaCategoria_){
    this._ListaCategoria_ = AListaCategoria_;
  }

  void _togleButtons(int AId){
    ButtonElement btnAceptar = _tableLista.tBodies[0].query('#btnAceptar${AId.toString()}');
    ButtonElement btnEditar = _tableLista.tBodies[0].query('#btnEditar${AId.toString()}');
    ButtonElement btnCancelar = _tableLista.tBodies[0].query('#btnCancelar${AId.toString()}');
    ButtonElement btnEliminar = _tableLista.tBodies[0].query('#btnEliminar${AId.toString()}');
    btnEditar.classes.toggle('oculto');
    btnAceptar.classes.toggle('oculto');
    btnCancelar.classes.toggle('oculto');
    btnEliminar.classes.toggle('oculto');
  }

  void addCategoria_(Categoria_ ACategoria_, int AIndex){
    TableRowElement tr = new TableRowElement()..id = 'tr${ACategoria_.Id.toString()}';
    TableCellElement td = new TableCellElement()..text = ACategoria_.Id.toString();
    td.classes.add('oculto');
    tr.children.add(td);

    td = new TableCellElement()..text = ACategoria_.Categoria;
    tr.children.add(td);
    td = new TableCellElement()..text = ACategoria_.Activo;
    tr.children.add(td);

    String pHtmlBtn = "<button class='btn button_edit''><i class='icon-edit'></i></button>";
    ButtonElement btn = new Element.html(pHtmlBtn)..id = 'btnEditar${ACategoria_.Id.toString()}'..onClick.listen((_){

      this.editCategoria_(ACategoria_);

    });

    td = new TableCellElement()..children.add(btn);

    pHtmlBtn = "<button class='btn button_edit'><i class='icon-ok'></i></button>";
    btn = new Element.html(pHtmlBtn)..id = 'btnAceptar${ACategoria_.Id.toString()}'..onClick.listen((_){

      bool pPuedeAgregar = true;
      TableRowElement tr = _tableLista.tBodies[0].query('#tr${ACategoria_.Id.toString()}');

      InputElement pInputCategoria = tr.cells[1].children[0];
      SelectElement pInputActivo = tr.cells[2].children[0];

      if (pPuedeAgregar){
        Categoria_ pCategoria_ = new Categoria_(ACategoria_.Id, pInputCategoria.value, pInputActivo.value, true);
        this._ListaCategoria_.edit(pCategoria_);

        tr.cells[1].children.clear();

        tr.cells[1].text = pCategoria_.Categoria;
        tr.cells[2].text = pCategoria_.Activo;

        _togleButtons(pCategoria_.Id);

      };
    });
    btn.classes.toggle('oculto');
    td.children.add(btn);

    pHtmlBtn = "<button class='btn'><i class='icon-remove'></i></button>";
    btn = new Element.html(pHtmlBtn)..id = 'btnCancelar${ACategoria_.Id.toString()}'..onClick.listen((_){

      Categoria_ pCategoria_ = this._ListaCategoria_.get(ACategoria_.Id);
      TableRowElement tr = _tableLista.tBodies[0].query('#tr${ACategoria_.Id.toString()}');
      tr.cells[1].children.clear();
      tr.cells[1].text = pCategoria_.Categoria;
      tr.cells[2].text = pCategoria_.Activo;

      _togleButtons(pCategoria_.Id);
    });
      btn.classes.toggle('oculto');
      td.children.add(btn);

      pHtmlBtn = "<button class='btn'><i class='icon-trash'></i></button>";
      btn = new Element.html(pHtmlBtn)..id='btnEliminar${ACategoria_.Id.toString()}'..onClick.listen((_) {
        if (window.confirm('¿Eliminar Categoria_?')){
          this._ListaCategoria_.remove(ACategoria_.Id);
        }
      });
      td.children.add(btn);
      tr.children.add(td);

      if (AIndex == 0){
        this._tableLista.tBodies[0].children.add(tr);
      }
      else{
        this._tableLista.tBodies[0].children.insert(0, tr);
      }
  }

  void remove(int AId){
    _tableLista.tBodies[0].children.remove(query('#tr${AId.toString()}'));
  }

  void setCategoria_(Categoria_ ACategoria_){
    TableRowElement tr = _tableLista.tBodies[0].query('#tr${ACategoria_.Id.toString()}');
    tr.cells[1].text = ACategoria_.Categoria;
    tr.cells[1].text = ACategoria_.Activo;
  }

  void editCategoria_(Categoria_ ACategoria_){
    TableRowElement tr = _tableLista.tBodies[0].query('#tr${ACategoria_.Id.toString()}');
    _togleButtons(ACategoria_.Id);

    tr.cells[1].text = '';
    InputElement pInputCategoria = new InputElement()..value = ACategoria_.Categoria;
    tr.cells[1].children.add(pInputCategoria);
    tr.cells[2].text = '';
    SelectElement pInputActivo = new SelectElement();
    pInputActivo.appendHtml('<option value="Si">Si</option>');
    pInputActivo.appendHtml('<option value="No">No</option>');
    pInputActivo.classes.add('select_edit');
    pInputActivo.value = ACategoria_.Activo;
    pInputActivo.classes.add('select_edit');
    tr.cells[2].children.add(pInputActivo);
    pInputCategoria.focus();
  }
}