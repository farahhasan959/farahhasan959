function barcodeGen() {

    var data = document.querySelector('.input').value;

    JsBarcode('#barcode', data , {

        background : '#fff',

        color : '#000',

        height : 100,

        displayValue : false

    });

}

