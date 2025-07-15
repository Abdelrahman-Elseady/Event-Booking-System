import "./SubmitMessage.css";
export default function SubmitMessage({ isVisible,message }) {
  if (isVisible) {
    return (
      <div className="SubmitMessage">
        <h2>Thank you for your request!</h2>
      </div>
    );
  }
  return <></>;
}
