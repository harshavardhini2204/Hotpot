import api from "./api";

const getUsers=async()=>{
    const response=await api.get("/User");
    return response.data;
}
const getProfile = async () => {
    const response = await api.get("/User/profile");
    return response.data;

};
const updateProfile = async (id, userData) => {
    const response = await api.put(
        `/User/${id}`,
        userData
    );
    return response.data;
};


export default {getUsers,getProfile,updateProfile}