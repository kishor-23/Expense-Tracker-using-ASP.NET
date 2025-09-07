$(document).ready(function () {
    let table = $('#expensesTable').DataTable();

    // Load Google Charts
    google.charts.load('current', { packages: ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);

    function drawChart() {
        $.getJSON('/Expenses/GetExpenseChartData', function (data) {
            const chartData = [['Category', 'Total']];
            data.forEach(item => chartData.push([item.Category, item.Total]));

            const options = { title: 'Expenses by Category', pieHole: 0.4 };
            const chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            chart.draw(google.visualization.arrayToDataTable(chartData), options);
        });
    }

    function refreshTable() {
        $.get('/Expenses/Index', function (html) {
            const tbody = $(html).find('#expensesTable tbody').html();
            $('#expensesTable tbody').html(tbody);
            table.destroy();
            table = $('#expensesTable').DataTable();
            attachHandlers();
            drawChart();
        });
    }

    // Create
    $('#createExpenseBtn').click(function () {
        $.get('/Expenses/Create', function (html) {
            Swal.fire({
                title: 'Create Expense',
                html: html,
                showCancelButton: true,
                confirmButtonText: 'Save',
                preConfirm: () => {
                    return new Promise((resolve, reject) => {
                        const form = $('#createExpenseForm');
                        $.post('/Expenses/Create', form.serialize())
                            .done(resolve)
                            .fail(reject);
                    });
                }
            }).then(result => { if (result.isConfirmed) refreshTable(); });
        });
    });

    // Edit/Delete
    function attachHandlers() {
        // Edit
        $('.editExpenseBtn').off('click').on('click', function () {
            const id = $(this).data('id');
            $.get(`/Expenses/Edit/${id}`, function (html) {
                Swal.fire({
                    title: 'Edit Expense',
                    html: html,
                    showCancelButton: true,
                    confirmButtonText: 'Save',
                    preConfirm: () => {
                        return new Promise((resolve, reject) => {
                            const form = $('#editExpenseForm');
                            $.post(`/Expenses/Edit/${id}`, form.serialize())
                                .done(resolve)
                                .fail(reject);
                        });
                    }
                }).then(result => { if (result.isConfirmed) refreshTable(); });
            });
        });

        // Delete
        $('.deleteExpenseBtn').off('click').on('click', function () {
            const id = $(this).data('id');
            Swal.fire({
                title: 'Delete?',
                text: 'This cannot be undone.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete!'
            }).then(result => {
                if (result.isConfirmed) {
                    $.post(`/Expenses/Delete/${id}`, function (data) {
                        if (data.success) refreshTable();
                        else Swal.fire('Error', data.message, 'error');
                    });
                }
            });
        });
    }

    attachHandlers();
});
