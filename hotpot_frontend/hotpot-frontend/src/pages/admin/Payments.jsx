import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import paymentService from "../../services/paymentService";

function Payments() {

    const [payments, setPayments] = useState([]);
    const [loading, setLoading] = useState(true);

    const fetchPayments = async () => {

        try {

            const data = await paymentService.getAllPayments();

            setPayments(data);

        }
        catch (error) {

            console.error(error);

            toast.error("Unable to load payments");

        }
        finally {

            setLoading(false);

        }

    };

    useEffect(() => {

        fetchPayments();

    }, []);

    const updateStatus = async (paymentId, status) => {

        try {

            await paymentService.updatePayment(
                paymentId,
                status
            );

            setPayments(
                payments.map(payment =>
                    payment.paymentId === paymentId
                        ? {
                            ...payment,
                            paymentStatus: status
                        }
                        : payment
                )
            );

            toast.success("Payment updated successfully");

        }
        catch (error) {

            console.error(error);

            toast.error("Unable to update payment");

        }

    };

    const deletePayment = async (id) => {

        try {

            await paymentService.deletePayment(id);

            setPayments(
                payments.filter(
                    payment => payment.paymentId !== id
                )
            );

            toast.success("Payment deleted");

        }
        catch (error) {

            console.error(error);

            toast.error("Unable to delete payment");

        }

    };

    if (loading) {

        return (
            <h3 className="text-center mt-5">
                Loading Payments...
            </h3>
        );

    }

    return (

    <div className="container py-4">

        <div className="payments-banner">

            <h1>💳 Payment Management</h1>

            <p>
                Monitor all transactions across the platform
            </p>

        </div>

        <div className="card shadow-sm border-0 rounded-4">

            <div className="card-body">

                <div className="d-flex justify-content-between align-items-center mb-4">

                    <h4 className="fw-bold mb-0">
                        Payments
                    </h4>

                    <span className="badge bg-primary fs-6">
                        {payments.length} Payments
                    </span>

                </div>

                <div className="table-responsive">

                    <table className="table align-middle payment-table">

                        <thead>

                            <tr>

                                <th>ID</th>
                                <th>Order</th>
                                <th>Amount</th>
                                <th>Method</th>
                                <th>Status</th>
                                <th>Date</th>
                                <th>Actions</th>

                            </tr>

                        </thead>

                        <tbody>

                            {payments.map(payment => (

                                <tr key={payment.paymentId}>

                                    <td>
                                        #{payment.paymentId}
                                    </td>

                                    <td>
                                        #{payment.orderId}
                                    </td>

                                    <td className="fw-bold text-success">
                                        ₹{payment.amount}
                                    </td>

                                    <td>

                                        <span className="method-badge">
                                            {payment.paymentMethod}
                                        </span>

                                    </td>

                                    <td>

                                        <span
                                            className={`badge ${
                                                payment.paymentStatus === "Completed"
                                                    ? "bg-success"
                                                    : "bg-warning text-dark"
                                            }`}
                                        >
                                            {payment.paymentStatus}
                                        </span>

                                    </td>

                                    <td>
                                        {new Date(
                                            payment.paymentDate
                                        ).toLocaleDateString()}
                                    </td>

                                    <td>

                                        <div className="payment-actions">

                                            <button
                                                className="btn btn-success btn-sm"
                                                onClick={() =>
                                                    updateStatus(
                                                        payment.paymentId,
                                                        "Completed"
                                                    )
                                                }
                                            >
                                                Complete
                                            </button>

                                            <button
                                                className="btn btn-warning btn-sm"
                                                onClick={() =>
                                                    updateStatus(
                                                        payment.paymentId,
                                                        "Pending"
                                                    )
                                                }
                                            >
                                                Pending
                                            </button>

                                            <button
                                                className="btn btn-danger btn-sm"
                                                onClick={() =>
                                                    deletePayment(
                                                        payment.paymentId
                                                    )
                                                }
                                            >
                                                Delete
                                            </button>

                                        </div>

                                    </td>

                                </tr>

                            ))}

                        </tbody>

                    </table>

                </div>

            </div>

        </div>

    </div>

);

}

export default Payments;