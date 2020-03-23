// Accessing the objects
var ctx = document.getElementById('monthlySales').getContext('2d');
var pieCtx = document.getElementById('deptSales').getContext('2d');
var yearlyLabel = document.getElementById('yearlyTotal');
var newAmount = document.getElementById('itemAmount');
var newMonth = document.getElementById('monthId');
let hikingRadio = document.getElementById('hiking');
let runningRadio = document.getElementById('running');
let huntingRadio = document.getElementById('hunting');

// Monthly Totals
var yearlyTotal = 0;

const monthlySales = new Map();

{
	let salesA = {
		a:[1,2]
	}

	var map = new WeakMap();
	map.set(salesA, 'Hiking');
 
	console.log('First ' + salesA)
}

// console.log('Second ' + salesA);

// Add Sales
function addSale(){
	monthlySales.set(newMonth.value, parseInt(newAmount.value))
	
	// Update our labels
	monthlySalesChart.data.labels = Array.from(monthlySales.keys());

	yearlyTotal = 0;

	monthlySalesChart.data.datasets.forEach((dataset) => {
	    dataset.data = [];
	});

	for (let amount of monthlySales.values()){
		yearlyTotal = amount + yearlyTotal;
		yearlyLabel.innerHTML = yearlyTotal;

		monthlySalesChart.data.datasets.forEach((dataset) => {
	        dataset.data.push(amount);
	    });
	}

	monthlySalesChart.update();

}

function findSale(){
	
}

function fillValue(){
	
}
// Bar chart
var monthlySalesChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: [],
        datasets: [{
            label: '# of Sales',
            data: [],
            backgroundColor: [
                'rgba(238, 184, 104, 1)',
                'rgba(75, 166, 223, 1)',
                'rgba(239, 118, 122, 1)',
            ],
            borderWidth: 0
        }]
    },
    options: {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    }
});

// Pie Chart
// var deptSalesChart = new Chart(pieCtx, {
//     type: 'pie',
//     data: {
//         labels: deptLabels,
//         datasets: [{
//             label: '# of Sales',
//             data: deptSales,
//             backgroundColor: [
//                 'rgba(238, 184, 104, 1)',
//                 'rgba(75, 166, 223, 1)',
//                 'rgba(239, 118, 122, 1)',
//             ],
//             borderWidth: 0
//         }]
//     },
//     options: {
        
//     }
// })