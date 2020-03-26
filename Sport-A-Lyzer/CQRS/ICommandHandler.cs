using System.Threading.Tasks;

namespace Sport_A_Lyzer.CQRS
{
	/// <summary>
	/// Yleinen rajapinta kaikille command handlereille
	/// </summary>
	/// <typeparam name="TCommand">Komennon tyyppi.</typeparam>
	public interface ICommandHandler<in TCommand>
	{
		/// <summary>
		/// Suorittaa komennon
		/// </summary>
		/// <param name="command">Suoritettava komento.</param>
		Task HandleAsync( TCommand command );
	}
}
