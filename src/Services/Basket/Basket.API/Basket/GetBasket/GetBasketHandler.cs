using Basket.API.Data;
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler (IBasketRepository repository) 
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //TOD: get basket from database
            // var basket = await _ repository.GetBasket(request.Username);
            var basket = await repository.GetBasket(query.UserName);
            return new GetBasketResult(basket);
        }
    }
}
