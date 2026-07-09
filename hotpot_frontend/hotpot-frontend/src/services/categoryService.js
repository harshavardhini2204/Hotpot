import api from "./api";

const getAllCategories = async () => {
    const response = await api.get("/Category");
    return response.data;
};
const getCategoryById=async(id)=>{
    const response=await api.get(`/Category/${id}`);
    return response.data;
};
const createCategory=async(category)=>{
    const response=await api.post("/Category",category);
    return response.data;
};
const updateCategory=async(id,category)=>{
    const response=await api.put(`/Category/${id}`,category);
    return response.data;
};
const deleteCategory=async(id)=>{
    const response=await api.delete(`/Category/${id}`);
    return response.data;
};



export default {
    getAllCategories,getCategoryById,createCategory,
    updateCategory,deleteCategory
};