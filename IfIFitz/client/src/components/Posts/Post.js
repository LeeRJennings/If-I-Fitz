import { Button, Card, CardBody, CardTitle, CardSubtitle, CardText } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { addFavorite, deleteFavorite } from "../../modules/postManager";
import { dateFormatter } from "../../Helpers/dateFormatter";

export const Post = ({ post, user, userFavorites, render, setRender }) => {
    const navigate = useNavigate()

    const handleAddFavorite = (id) => {
        addFavorite(id)
        .then(() => setRender(render + 1))
    }

    const handleDeleteFavorite = (id) => {
        deleteFavorite(id)
        .then(() => setRender(render + 1))
    }

    const buttonDisplay = () => {
        if (user.id === post.userProfileId) {
            return (
                <>
                <Button color="primary" onClick={() => navigate(`/posts/edit/${post.id}`)}>Edit</Button>
                <Button color="danger" onClick={() => navigate(`/posts/delete/${post.id}`)}>Delete</Button>
                </>
            )
        } else {
            if (userFavorites.find((f) => f.id === post.id)) {
                return (
                    <div type="button" className="bggray2 text-info">
                        <i className="fa-solid fa-star fa-xl" onClick={() => handleDeleteFavorite(post.id)}></i>
                    </div>
                )
            } else {
                return (
                    <Button color="info" onClick={() => handleAddFavorite(post.id)}>I Sitz</Button>
                )
            }
        }
    }

    return (
        <Card color="light" style={{width: '20rem'}}>
            <CardBody>
                <CardTitle tag="h5">
                    {post.title}
                </CardTitle>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                    Posted by: {post.userProfile.name} <br/> On: {dateFormatter(post.createdDateTime)}
                </CardSubtitle>
            </CardBody>
                <img alt="probably a cat in a box" src={post.imageLocation} width="100%"/>
            <CardBody>
                <CardText className="mb-2 text-muted">
                    <b>Size:</b> {post.size.name} | <b>Material:</b> {post.material.type}
                </CardText>
                <CardText>
                    {post.description}
                </CardText>
                <div>    
                    {buttonDisplay()}
                    <div>
                        <Button onClick={() => navigate(`/posts/${post.id}`)}>See Comments</Button>
                    </div>
                </div>
            </CardBody>
        </Card>
    )
}