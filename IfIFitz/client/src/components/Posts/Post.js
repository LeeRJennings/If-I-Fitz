import { Button, Card, CardBody, CardTitle, CardSubtitle, CardText } from "reactstrap";
import { useNavigate, useLocation } from "react-router-dom";
import { addFavorite, deleteFavorite } from "../../modules/postManager";
import { dateFormatter } from "../../helpers/dateFormatter";

export const Post = ({ post, user, userFavorites, render, setRender }) => {
    const navigate = useNavigate()
    const location = useLocation()

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
                <div className="editAndDelete">
                    <div type="button" className="bggray2 text-primary star">
                        <i className="fa-solid fa-pen-to-square fa-xl" onClick={() => navigate(`/posts/edit/${post.id}`)}></i>
                    </div>
                    <div type="button" className="bggray2 text-danger star">
                        <i className="fa-solid fa-trash-can fa-xl" onClick={() => navigate(`/posts/delete/${post.id}`)}></i>
                    </div>
                </div>
            )
        } else {
            if (userFavorites.find((f) => f.id === post.id)) {
                return (
                    <div type="button" className="bggray2 text-info star">
                        <i className="fa-solid fa-star fa-xl" onClick={() => handleDeleteFavorite(post.id)}></i>
                    </div>
                )
            } else {
                return (
                    <div type="button" className="bggray2 text-info star">
                        <i className="fa-regular fa-star fa-xl" onClick={() => handleAddFavorite(post.id)}></i>
                    </div>
                )
            }
        }
    }

    const commentButtonDisplay = () => {
        if (location.pathname === `/posts/${post.id}`) {
            return ("")
        } else {
            return (
                <Button onClick={() => navigate(`/posts/${post.id}`)}>See Comments</Button>
            )
        }
    }

    return (
        <Card className="m-2 cardPost" color="light" style={{width: '20rem'}}>
            <CardBody className="pb-0">
                <CardTitle tag="h5">
                    {post.title}
                </CardTitle>
                <CardSubtitle className="pb-1 text-muted" tag="h6">
                    Posted by: {post.userProfile.name} <br/> On: {dateFormatter(post.createdDateTime)}
                </CardSubtitle>
            </CardBody>
            <img alt="probably a cat in a box" src={post.imageLocation} />
            <CardBody>
                <CardText className="mb-2 text-muted">
                    <b>Size:</b> {post.size.name} | <b>Material:</b> {post.material.type}
                </CardText>
                <CardText>
                    {post.description}
                </CardText>
            </CardBody>
            <div className="cardButtons">    
                <div className="otherButtons">
                    {buttonDisplay()}
                </div>
                <div className="commentButton">
                    {commentButtonDisplay()}
                </div>
            </div>
        </Card>
    )
}