import api from "./api"

const getAllPayments=async()=>{
    const response=await api.get("/Payment");
    return response.data;
};
const getPaymentById=async(id)=>{
    const response=await api.get(`/Payment/${id}`);
    return response.data;
};
const createPayment = async (payment) => {
    const response = await api.post("/Payment", payment);
    return response.data;
};

const updatePayment = async (id, payment) => {
    const response = await api.put(`/Payment/${id}`, payment);
    return response.data;
};

const deletePayment = async (id) => {
    const response = await api.delete(`/Payment/${id}`);
    return response.data;
};

export default {
    getAllPayments,
    getPaymentById,
    createPayment,
    updatePayment,
    deletePayment
};