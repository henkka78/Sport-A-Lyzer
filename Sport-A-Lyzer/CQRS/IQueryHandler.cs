using System.Threading.Tasks;

namespace Sport_A_Lyzer.CQRS
{
	/// <summary>
	/// Rajapinta kaikille QueryHandlereille
	/// </summary>
	/// <typeparam name="TQuery">Queryn tyyppi.</typeparam>
	/// <typeparam name="TResult">Paluuarvon tyyppi.</typeparam>
	public interface IQueryHandler<in TQuery, TResult>
	{
		Task<TResult> HandleAsync( TQuery query );
	}
}
