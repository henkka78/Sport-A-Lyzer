using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_A_Lyzer.Services
{
	public static class DatabaseConstants
	{
		internal sealed class GameEventTypeId : TypeSafeEnumBase<GameEventTypeId>
		{
			public static readonly GameEventTypeId Goal = new GameEventTypeId( "10159085-2f0b-4faa-8ac7-a9f00ecafe1b" );
			public static readonly GameEventTypeId Assist = new GameEventTypeId( "0ca1d4ee-1a58-4c59-8b84-e8a9053471fb" );

			/// <summary>
			/// Initializes the <see cref="GameEventTypeId"/> class.
			/// Jos tätä staattista konstruktoria ei ole, niin Release-buildissa tämä luokka merkitään "beforefieldinit":ksi, 
			/// ja sen type initializereita ei kutsuta ennen kuin ensimmäistä kertaa käytetään tämän luokan membereitä. 
			/// Staattisen funktion kutsuminen siis ei laukaise niitä, eikä siten täytä Instance-luokkamuuttujaa, 
			/// joka aiheuttaa ajonaikaisia virheitä. Ks. http://csharpindepth.com/Articles/General/Beforefieldinit.aspx
			/// </summary>
			static GameEventTypeId()
			{
			}

			private GameEventTypeId( string name ) : base( name )
			{
			}

			public static explicit operator GameEventTypeId( string str )
			{
				if ( Instance.TryGetValue( str, out var result ) )
					return result;
				else
					throw new InvalidCastException( );
			}
		}
	}

	/// <summary>
	/// Tyyppisuojatun Enum-imitaation pohjaluokka
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TypeSafeEnumBase<T> where T : TypeSafeEnumBase<T>
	{
		/// <summary>
		/// Singleton, joka sisältää kaikki tämän enum-tyypin vaihtoehdot
		/// </summary>
		protected static readonly Dictionary<string, T> Instance = new Dictionary<string, T>();
		/// <summary>
		/// Enum-vaihtoehdon nimi
		/// </summary>
		public string Name;

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeSafeEnumBase{T}"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		protected TypeSafeEnumBase( string name )
		{
			Name = name;
			Instance[ name ] = ( T )this;
		}

	}
}
