import api from "./api";

const getMenuItemsByRestaurant = async (restaurantId) => {

    const response = await api.get(
        `/MenuItem/restaurant/${restaurantId}`
    );

    return response.data;
};
const getAllMenuItems=async()=>{
    const response=await api.get("/MenuItem");
    return response.data;
};
const getMenuItemById=async(menuItemId)=>{
    const response=await api.get(`/MenuItem/${menuItemId}`);
    return response.data;
};
const searchMenuItem=async(keyword)=>{
    const response=await api.get(`/MenuItem/search?keyword=${keyword}`);
    return response.data
};
const createMenuItem=async(item)=>{
    const response=await api.post("/MenuItem",item);
    return response.data;
};
const updateMenuItem=async(id,item)=>{
    const response=await api.put(`/MenuItem/${id}`,item);
    return response.data;
};
const deleteMenuItem=async(id)=>{
    const response=await api.delete(`/MenuItem/${id}`);
    return response.data;
};

export default {
    getMenuItemsByRestaurant,getAllMenuItems,getMenuItemById,searchMenuItem,
    createMenuItem,updateMenuItem,deleteMenuItem
};