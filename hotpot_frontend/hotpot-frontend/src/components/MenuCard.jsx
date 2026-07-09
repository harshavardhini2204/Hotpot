import { Link } from "react-router-dom";

function MenuCard({ menuItem }) {

    return (

        <div className="col-md-4 mb-4">

            <div className="card h-100 shadow">

                <img
                    src={menuItem.imageUrl}
                    className="card-img-top"
                    alt={menuItem.itemName}
                    style={{ height: "220px", objectFit: "cover" }}
                />

                <div className="card-body">

                    <h5>{menuItem.itemName}</h5>

                    <p>{menuItem.description}</p>

                    <h6 className="text-success">

                        ₹{menuItem.discountPrice}

                    </h6>

                    <small className="text-muted">

                        ₹<del>{menuItem.price}</del>

                    </small>

                    <br /><br />

                    <Link
    to={`/menu/${menuItem.menuItemId}`}
    className="btn btn-primary w-100"
>

    View Details

</Link>

                </div>

            </div>

        </div>

    );

}

export default MenuCard;