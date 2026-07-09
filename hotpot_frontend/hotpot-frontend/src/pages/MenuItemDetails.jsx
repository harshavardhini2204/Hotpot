import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import menuService from "../services/menuService";
import cartService from "../services/cartService";
import { toast } from "react-toastify";


export function MenuItemDetails(){
    const{id}=useParams();
    const[menuItem,setMenuItem]=useState(null);
    const[quantity,setQuantity]=useState(1);
    const[loading,setLoading]=useState(true);

    useEffect(()=>{
        const fetchMenuItem=async()=>{
            try{
                const data=await menuService.getMenuItemById(id);
                setMenuItem(data);
            }
            catch(error){
                console.error(error);
            }
            finally{
                setLoading(false);
            }
        };
        fetchMenuItem();
    },[id]);
    const handleAddToCart=async()=>{
        try{
            const userId=Number(localStorage.getItem("userId"));
            await cartService.addToCart({
                userId,menuItemId:menuItem.menuItemId,quantity
            });
            toast.success("Added to Cart");
        }
        catch(error){
            console.error(error);
            toast.error("Unable to add item");
        }

    };
     if (loading)
        return <h3 className="text-center mt-5">Loading...</h3>;

    return (

        <div className="container mt-5">

            <div className="row">

                <div className="col-md-5">

                    <img
    src={menuItem.imageUrl}
    alt={menuItem.itemName}
    className="img-fluid rounded shadow"
    style={{
        width: "100%",
        maxHeight: "400px",
        objectFit: "cover"
    }}
/>

                </div>

                <div className="col-md-7">

                    <h2>{menuItem.itemName}</h2>

                    <p>{menuItem.description}</p>

                    <h4 className="text-success">

                        ₹{menuItem.discountPrice}

                    </h4>

                    <p>

                        <del>

                            ₹{menuItem.price}

                        </del>

                    </p>

                    <hr />

                    <p>

                        <strong>Ingredients :</strong>

                        {" "}

                        {menuItem.ingridients}

                    </p>

                    <p>

                        <strong>Cooking Time :</strong>

                        {" "}

                        {menuItem.cookingTime} mins

                    </p>

                    <p>

                        <strong>Dietary Type :</strong>

                        {" "}

                        {menuItem.dietaryType}

                    </p>

                    <p>

                        <strong>Taste :</strong>

                        {" "}

                        {menuItem.tasteInfo}

                    </p>

                    <hr />

                    <h5>Nutrition</h5>

                    <p>

                        Calories :
                        {" "}
                        {menuItem.calories}

                    </p>

                    <p>

                        Protein :
                        {" "}
                        {menuItem.protein} g

                    </p>

                    <p>

                        Carbs :
                        {" "}
                        {menuItem.carbs} g

                    </p>

                    <p>

                        Fat :
                        {" "}
                        {menuItem.fat} g

                    </p>

                    <div className="d-flex align-items-center mt-3">

                        <button
                            className="btn btn-secondary"
                            onClick={() =>
                                quantity > 1 &&
                                setQuantity(quantity - 1)
                            }
                        >

                            -

                        </button>

                        <span className="mx-3">

                            {quantity}

                        </span>

                        <button
                            className="btn btn-secondary"
                            onClick={() =>
                                setQuantity(quantity + 1)
                            }
                        >

                            +

                        </button>

                    </div>

                    <button
                        className="btn btn-success mt-4"
                        onClick={handleAddToCart}
                    >

                        Add To Cart

                    </button>

                </div>

            </div>

        </div>

    );
}