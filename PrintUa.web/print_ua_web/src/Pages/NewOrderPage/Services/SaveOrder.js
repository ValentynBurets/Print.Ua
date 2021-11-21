import axios from 'axios'

import ConnectionConfig from '../../../jsonData/ConnectionConfig.json'

const saveOrder = (myProductArray, comments) => {

    if(myProductArray == null){
        alert("Please add new product to create an order");
        return;
    } 

    let newOrder = {
        "products": [],
        "comments": [{"text": comments.toString()}]
    } 

    myProductArray.forEach(element => {
        newOrder.products.push({
            "picture": element.picture,
            "serviceId": element.serviceId,
            "amount": element.amount
        })
    });

    // comments.forEach(element => {
    //     newOrder.comments.push({
    //         "text": element
    //     })
    // });

    let token = localStorage.getItem('token')
    
    axios
    .post(`${ConnectionConfig.ServerUrl + ConnectionConfig.Routes.CreateOrder}`,
    newOrder, 
    {   
        headers: { 'Authorization': `Bearer ${token}` }
    })
    .then((responce) => {
        var data = responce.data
        console.log(data)
    })
    .catch((e) => {
        console.log(e)
        alert(e)
    })
}  

export default saveOrder
