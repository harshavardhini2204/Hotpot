import api from "./api"

const placeOrder=async(order)=>{
    const response=await api.post("/Order/placeOrder",order);
    return response.data;
};
const getMyOrder=async()=>{
    const response=await api.get("/Order/my-orders");
    return response.data;

}
const getAllOrders=async()=>{
    const response=await api.get("/Order");
    return response.data;
};
const getOrderById=async(id)=>{
    const response=await api.get(`/Order/${id}`);
    return response.data;
};
const updateOrderStatus = async (id, status) => {
    const response = await api.put(
        `/Order/status/${id}`,
        JSON.stringify(status),
        {
            headers: {
                "Content-Type": "application/json"
            }
        }
    );

    return response.data;
};
const deleteOrder=async(id)=>{
    const response=await api.delete(`/Order/${id}`);
    return response.data;
}
const getRestaurantOrders = async (restaurantId) => {
    const response =
        await api.get(`/Order/restaurant/${restaurantId}`);

    return response.data;
};
export default{
    getAllOrders,getOrderById,placeOrder,updateOrderStatus,deleteOrder,getMyOrder,getRestaurantOrders
}