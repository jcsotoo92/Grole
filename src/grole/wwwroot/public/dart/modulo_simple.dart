import 'dart:html';

void main() {
  
    (querySelector("#miboton") as ButtonElement).onClick.listen((_){
      
        window.alert("Escupelo");
      
    });
  
}

