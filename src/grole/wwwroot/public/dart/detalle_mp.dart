library DetalleMP;

class DetalleMP{
    int tarimaOrigen;
    String producto;
    String codigoBarras;
    double peso;
 
    DetalleMP.fromMAP(Map AMap){
        this.tarimaOrigen = AMap["tarima_origen"];
        this.producto     = AMap["producto"];
        this.codigoBarras = AMap["codigobarras"];
        this.peso         = AMap["peso"].toDouble();
    }
}