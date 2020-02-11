//WebSocket = undefined;
//EventSource = undefined;

setupConnection = (hubProxy) => {

    hubProxy.on("receiveOrderUpdate", function (updateObject) {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Order: ${updateObject.OrderId}: ${updateObject.Update}`;
    });

    hubProxy.on("newOrder", function (order) {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = `Somebody ordered an ${order.Product}`;
    });
};

$(document).ready(() => {
    var connection = $.hubConnection();
    var hubProxy = connection.createHubProxy('coffeeHub');

    setupConnection(hubProxy);
    connection.start();
    //connection.start({ transport: 'longPolling' });

    document.getElementById("submit").addEventListener("click",
        e => {
            e.preventDefault();
            var statusDiv = document.getElementById("status");
            statusDiv.innerHTML = "Submitting order..";

            const product = document.getElementById("product").value;
            const size = document.getElementById("size").value;

            fetch("api/Coffee",
                {
                    method: "POST",
                    body: JSON.stringify({ product, size }),
                    headers: {
                        'content-type': 'application/json'
                    }
                })
                .then(response => response.text())
                .then(id => hubProxy.invoke('getUpdateForOrder', { id, product, size })
                    .fail(error => console.log(error))
                );
        });
});