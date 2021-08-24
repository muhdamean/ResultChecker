/* cascading dropdown */
$(document).ready(function () {
    var state = $("#State");
    var lga = $("#lga");
    lga.prop('disabled', true);

    state.change(function () {
        if ($(this).val() == "") {
            lga.append($('<option>', { value: '', text: '---- Select LGA ----' }, '</option>'));
            lga.prop('disabled', true);
            lga.val("");
        }
        else {
            $.getJSON("/GetLga", { state: $(this).val() }, function (data) {
                lga.prop('disabled', false);
                lga.empty();
                lga.append($('<option>', { value: '0', text: '---- Select LGA ----' }, '</option>'));
                $.each(data, function (index, lgas) {
                    lga.append($('<option/>',
                        {
                            value: lgas.value,
                            text: lgas.text,
                        }));
                });
            });
            //$.ajax({
            //    url: "https://localhost:44369/GetLga",
            //    method: "GET",
            //    data: { state: $(this).val() },
            //    success: function (data) {
            //        /* console.log(data);*/
            //        lga.prop('disabled', false);
            //        lga.empty();
            //        lga.append($('<option>', { value: '0', text: '---- Select LGA ----' }, '</option>'));
            //        $(data).each(function (index, item) {
            //            lga.append($('<option>', { value: item.value, text: item.text }, '</option>'))
            //        });
            //        /* console.log("finish");*/
            //    }
            //});
        }
    });
});