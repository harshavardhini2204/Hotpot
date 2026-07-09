import api from "./api"

const addToCart=async (cartItem)=>{
    const response=await api.post("/Cart/add",cartItem);
    return response.data;
};
const getCart=async(userId)=>{
    const response=await api.get(`/Cart/${userId}`);
    return response.data;
};
const updateQuantity=async (cartItemId,quantity)=>{
   const response = await api.put(
    `/Cart/update/${cartItemId}`,
    {
        
        quantity: quantity
    }
);

return response.data;
};
const removeItem=async (cartItemId)=>{
    const response=await api.delete(`/Cart/remove/${cartItemId}`);
    return response.data;
};
export default{
    addToCart,getCart,updateQuantity,removeItem
};

