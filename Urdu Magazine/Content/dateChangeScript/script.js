function changeDateToUrdu(){

   $('.changeDateFormat').each(function () {
                    var date = new Date($(this).attr('date'));
                    var month = date.getMonth();
                    var year = date.getFullYear()+"";
                    var yearInUrdu = "";
                    var monthInUrdu = "";
                    for (var i = 0; i < year.length; i++) {

                        var a = typeUrdu(year.charCodeAt(i), String.fromCharCode(year.charCodeAt(i)));
                        console.log(a.newKey);
                        yearInUrdu += a.newKey;

                    }
                    if (month == 1) {
                        monthInUrdu = "جنوری";
                    }
                    else if (month == 2) {
                        monthInUrdu = "فروری";
                    }
                    else if (month == 3) {
                        monthInUrdu = "مارچ";
                    }
                    else if (month == 4) {
                        monthInUrdu = "اپریل";
                    }
                    else if (month == 5) {
                        monthInUrdu = "مئی";
                    }
                    else if (month == 6) {
                        monthInUrdu = "جون";
                    }
                    else if (month == 7) {
                        monthInUrdu = "جولائی";
                    }
                    else if (month == 8) {
                        monthInUrdu = "اگست";
                    }
                    else if (month == 9) {
                        monthInUrdu = "ستمبر";
                    }
                    else if (month == 10) {
                        monthInUrdu = "اکتوبر";
                    }
                    else if (month == 11) {
                        monthInUrdu = "نومبر";
                    }
                    else if (month == 12) {
                        monthInUrdu = "دسمبر";
                    }
                    $(this).text(yearInUrdu+" "+monthInUrdu);
                })
}