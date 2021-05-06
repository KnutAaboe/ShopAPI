////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Threading.Tasks;

////namespace ShopApi.wwwroot.js
////{
////    public class site
////    {
////    }
////}

const uri = 'api/Items';
let items = [];

function getItems() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayItems(data)) //Invoked when API returns successful status code. 
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-Name');
    const addAmountTextbox = document.getElementById('add-Amount');
    const addLocationTextbox = document.getElementById('add-Location');

    const item = {
        name: addNameTextbox.value.trim(),
        amount: addAmountTextbox.value.trim(),
        location: addLocationTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addAmountTextbox.value = '';
            addLocationTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = items.find(item => item.id === id);

    document.getElementById('edit-Id').value = item.id;
    document.getElementById('edit-Name').value = item.name;
    document.getElementById('edit-Amount').value = item.amount;
    document.getElementById('edit-Location').value = item.location;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-Id').value;
    const item = {
        id: parseInt(itemId, 10),
        name: document.getElementById('edit-Name').value.trim(),
        amount: document.getElementById('edit-Amount').value.trim(),
        location: document.getElementById('edit-Location').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'groceries' : 'grocerie'; 

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('items');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');


    data.forEach(item => {

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td0 = tr.insertCell(0);
        let textNodeName = document.createTextNode(item.name);
        td0.appendChild(textNodeName);

        let td1 = tr.insertCell(1);
        let textNodeAmount = document.createTextNode(item.amount);
        td1.appendChild(textNodeAmount);

        let td2 = tr.insertCell(2);
        let textNodeLocation = document.createTextNode(item.location);
        td2.appendChild(textNodeLocation);

        let td3 = tr.insertCell(3);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(4);
        td4.appendChild(deleteButton);



    });

    items = data;
}