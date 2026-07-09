import api from "./api";

const register=async(data)=>{

    return await api.post("/Auth/register",data);
};

const login =async (data)=>{
    return await api.post("/Auth/login",data);
};

export default { register,login};