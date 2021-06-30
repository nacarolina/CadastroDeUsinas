﻿const Fornecedores = [];

$(function () {
    CarregarLista();
    $.get("/Usinas/ObterFornecedores", null, function (data) {

        for (var i = 0; i < data.length; i++) {
            var lst = data[i];
            var forne = { nome: lst.nome, id: lst.id };

            Fornecedores.push(forne);
        }

        autocomplete(document.getElementById("txtFornecedor"), Fornecedores);
    });
});

function CarregarLista() {
    $.get("/Usinas/ObterUsinas", null, function (data) {

        $("#tbUsinas").empty();

        if (data.length == 0)
            $("#tfUsinas").css("display", "");
        else
            $("#tfUsinas").css("display", "none");

        for (var i = 0; i < data.length; i++) {
            var lst = data[i];
            var row = $("<tr>");
            var cols = "<td>" + lst.uc + "</td>";

            cols += "<td style='width: 5%;'><div class='form-check' style='text-align: center;'><input class='form-check-input position-static' disabled type='checkbox' checked='" + lst.ativo + "' id='blankCheckbox' value='opcao1'></div></td>";
            cols += "<td>" + lst.idFornecedor + "</td>";
            cols += "<td style='padding: 10px; width: 8%;'><div class='btn-group' role='group'>" +
                "<button id ='btnGroupDrop1' type='button' class='btn btn-danger dropdown-toggle' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>" +
                "<i class='fas fa-bars'></i>" +
                "</button >" +
                "<div class='dropdown-menu' aria-labelledby='btnGroupDrop1'>" +
                "<a class='dropdown-item' onclick='Editar(this)' data-id='" + lst.id + "' data-uc='" + lst.uc + "' data-idfornecedor='" + lst.idFornecedor +
                "' data-fornecedor='"+lst.fornecedor+"' data-ativo='"+lst.ativo+"'>Editar</a>" +
                "<a asp-for='ID' class='dropdown-item' onclick='Excluir(this)' data-id='" + lst.id + "' data-uc='" + lst.uc + "' data-idfornecedor='" + lst.idFornecedor +
                "' data-fornecedor='" + lst.fornecedor + "' data-ativo='" + lst.ativo +"'>Excluir</a>" +
                "</div> </div ></td>";
            row.append(cols);
            $("#tbUsinas").append(row);
        }
    });
}
function Cadastrar() {
    $('#txtUCusina').trigger('focus');
    $("#txtUCusina").val("");
    $("#txtFornecedor").val("");
    $("#chkAtivo")[0].checked = true;
    $('#mdCadastro').modal('show');
}
function Salvar() {
    var UC = "", IdFornecedor = "", Ativo = "", Id = "", url = "", data = [];
    //#region valida campos em branco
    if ($("#txtUCusina").val() == "") {

    }

    //#endregion

    UC = $("#txtUCusina").val();
    IdFornecedor = $("#txtFornecedor")[0].dataset.id;
    Ativo = $("#chkAtivo")[0].checked;

    if ($("#btnSalvar")[0].outerText != "Salvar") {
        url = "/Usinas/SalvarAlteracoes";
        Id = $("#btnSalvar")[0].dataset.id;
        data = { UC: UC, Ativo: Ativo, IdFornecedor: IdFornecedor, Id: Id };
    }
    else {
        url = "/Usinas/Salvar";
        data = { UC: UC, Ativo: Ativo, IdFornecedor: IdFornecedor };
    }

    $.ajax({
        url: url,
        data: data,
        type: "POST",
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        success: function (data, textStatus, XMLHttpRequest) {

            if (data.d == "existe") {

            }
            else {
                $("#mdCadastro").modal("hide");
                CarregarLista();
            }
        }
    });
}

//#region autocomplete
let autocomplete = (inp, arr) => {
    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    let currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        let a, //OUTER html: variable for listed content with html-content
            b, // INNER html: filled with array-Data and html
            i, //Counter
            val = this.value;

        /*close any already open lists of autocompleted values*/
        closeAllLists();

        if (!val) {
            return false;
        }

        currentFocus = -1;

        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");

        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items list-group text-left");

        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);

        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].nome.toUpperCase().includes(val.toUpperCase())) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                b.setAttribute("class", "list-group-item list-group-item-action");
                /*make the matching letters bold:*/
                b.innerHTML += arr[i].nome;
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i].nome + "' data-id='" + arr[i].id + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    inp.dataset.id = this.getElementsByTagName("input")[0].dataset.id;
                    /*close the list of autocompleted values,
                        (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });

    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
              increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) {
            //up
            /*If the arrow UP key is pressed,
              decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });

    let addActive = (x) => {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = x.length - 1;
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("active");
    }

    let removeActive = (x) => {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (let i = 0; i < x.length; i++) {
            x[i].classList.remove("active");
        }
    }

    let closeAllLists = (elmnt) => {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }

    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });

};
//#endregion