import useFetchHouses from "../hooks/HouseHooks";
import { currencyFormatter } from "../config";
import ApiStatus from "../apiStatus";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import useFetchUser from "../hooks/UserHooks";

const HouseList = () => {
  const nav = useNavigate();
  const { data, status, isSuccess } = useFetchHouses();
  const { data: userClaims } = useFetchUser();

  if (!isSuccess) return <ApiStatus status={status} />;

  return (
    <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Houses currently on the market
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {data &&
            data.map((h) => (
              <tr key={h.id} onClick={() => nav(`/house/${h.id}`)}>
                <td>{h.address}</td>
                <td>{h.country}</td>
                <td>{currencyFormatter.format(h.price)}</td>
              </tr>
            ))}
        </tbody>
      </table>
      {userClaims &&
        userClaims.find((c) => c.type === "role" && c.value === "Admin") && (
          <Link className="btn btn-primary" to="/house/add">
            Add
          </Link>
        )}
    </div>
  );
};

export default HouseList;
