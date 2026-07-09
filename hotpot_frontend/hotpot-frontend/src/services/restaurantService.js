import api from "./api";

const getRestaurants = async () => {
    const response = await api.get("/Restaurant");
    return response.data;
};

const getRestaurantById = async (id) => {
    const response = await api.get(`/Restaurant/${id}`);
    return response.data;
};
const getRestaurantByOwner = async (userId) => {
    const response = await api.get(
        `/Restaurant/owner/${userId}`
    );
    return response.data;
};

const searchRestaurants = async (keyword) => {
    const response = await api.get(`/Restaurant/search?keyword=${keyword}`);
    return response.data;
};

const createRestaurant = async (restaurant) => {
    const response = await api.post("/Restaurant", restaurant);
    return response.data;
};

const updateRestaurant = async (id, restaurant) => {
    const response = await api.put(`/Restaurant/${id}`, restaurant);
    return response.data;
};

const deleteRestaurant = async (id) => {
    const response = await api.delete(`/Restaurant/${id}`);
    return response.data;
};

export default {
    getRestaurants,
    getRestaurantById,
    getRestaurantByOwner,
    searchRestaurants,
    createRestaurant,
    updateRestaurant,
    deleteRestaurant
};